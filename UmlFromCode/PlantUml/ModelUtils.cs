// Copyright 2019 Jose Luis Rovira Martin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AgileUml.Model;

namespace UmlFromCode.PlantUml
{
    public static class ModelUtils
    {
        public static IEnumerable<Type> GetClasses(this Assembly assembly)
        {
            return assembly.DefinedTypes.Where(type => !type.IsSpecialName);
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type @class)
        {
            return @class.GetConstructors(publicBinding).Where(constructor => !constructor.IsSpecialName);
        }

        public static IEnumerable<MethodInfo> GetMethods(Type @class)
        {
            return @class.GetMethods(publicBinding).Where(method => !method.IsSpecialName);
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type @class)
        {
            // Is not a special name, nor an association end.
            bool NotSpecialNorAssociationEnd(PropertyInfo property)
            {
                return !property.IsSpecialName && property.GetCustomAttribute<AssociationEndAttribute>() == null;
            }

            return @class.GetProperties(publicBinding).Where(NotSpecialNorAssociationEnd);
        }

        public static IEnumerable<EventInfo> GetEvents(Type @class)
        {
            return @class.GetEvents(publicBinding).Where(@event => !@event.IsSpecialName);
        }

        public static IEnumerable<FieldInfo> GetFields(Type @class)
        {
            // Is not a special name, nor an association end.
            bool NotSpecialNorAssociationEnd(FieldInfo field)
            {
                return !field.IsSpecialName && field.GetCustomAttribute<AssociationEndAttribute>() == null;
            }

            return @class.GetFields(publicBinding).Where(NotSpecialNorAssociationEnd);
        }

        public static IEnumerable<Generalization> GetGeneralizations(Type @class)
        {
            Type baseType = @class.BaseType;
            if ((baseType != null) && !IsSpecial(baseType))
            {
                yield return CreateInheritance(baseType, @class);
            }

            foreach (Type @interface in @class.GetDirectInterfaces())
            {
                yield return CreateInheritance(@interface, @class);
            }
        }

        public static IEnumerable<Association> GetAssociations(Type @class)
        {
            return @class.GetProperties(publicBinding)
                .Improve(property => property.GetCustomAttribute<AssociationEndAttribute>()) // Tuple<PropertyInfo, AssociationEndAttribute>
                .Where(t => t.Item2 != null)
                .Select(t => CreateAssociation(t.Item1, t.Item2))
                .NotNull();
        }

        public static IEnumerable<Dependency> GetDependencies(Type @class)
        {
            return @class.GetCustomAttributes<DependencyAttribute>(false)
                .Select(attr => CreateDependency(@class, attr));
        }

        public static ClassType ToClassType(Type @class)
        {
            ClassType classType;
            if (@class.IsClass)
            {
                classType = ClassType.Class;
            }
            else if (@class.IsInterface)
            {
                classType = ClassType.Interface;
            }
            else if (@class.IsEnum)
            {
                classType = ClassType.Enum;
            }
            else if (@class.IsValueType)
            {
                classType = ClassType.Class;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
            return classType;
        }

        #region private

        private const BindingFlags publicBinding =
            BindingFlags.Static
            | BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.DeclaredOnly;

        private static bool IsSpecial(Type @class)
        {
            return @class == typeof(object)
                || @class == typeof(Enum)
                || @class == typeof(Delegate)
                || @class == typeof(ValueType);
        }

        private static Generalization CreateInheritance(Type baseClass, Type @class)
        {
            return new Generalization(baseClass, @class);
        }

        private static Association CreateAssociation(PropertyInfo property, AssociationEndAttribute attr)
        {
            Type otherEnd = attr.OtherEnd ?? property.PropertyType;

            if (otherEnd == null)
            {
                // Is it an error???
                return null;
            }

            string otherPropertyName = attr.OtherProperty ?? property.DeclaringType.Name;

            if (otherEnd != null && !string.IsNullOrEmpty(otherPropertyName))
            {
                PropertyInfo otherProperty = otherEnd.GetProperty(otherPropertyName, publicBinding);

                if (otherProperty != null)
                {
                    AssociationEndAttribute otherAttr = otherProperty.GetCustomAttribute<AssociationEndAttribute>();
                    if (otherAttr != null)
                    {
                        // Two ends.
                        return Association.New(
                            property.DeclaringType,
                            property,
                            ToEndType(attr.Type),
                            ToString(attr.Cardinality),

                            otherProperty.DeclaringType,
                            otherProperty,
                            ToEndType(otherAttr.Type),
                            ToString(otherAttr.Cardinality),

                            attr.Name ?? otherAttr.Name);
                    }
                }
            }

            // Only one end.
            return Association.New(
                property.DeclaringType,
                property,
                ToEndType(attr.Type),
                ToString(attr.Cardinality),

                otherEnd,

                attr.Name);
        }

        private static Dependency CreateDependency(Type @class, DependencyAttribute attr)
        {
            return new Dependency(@class, attr.Supplier, attr.Name);
        }

        private static string ToString(int[] cardinality)
        {
            if (cardinality == null || cardinality.Length == 0)
            {
                return "";
            }

            int c0 = cardinality[0];
            if (cardinality.Length == 1)
            {
                return c0.ToString("d");
            }

            int c1 = cardinality[1];
            if (c0 == 0 && c1 == int.MaxValue)
            {
                return "*";
            }
            if (c0 == 1 && c1 == int.MaxValue)
            {
                return "+";
            }
            return string.Format("{0}..{1}",
                c0.ToString("d"),
                c1 != int.MaxValue ? c1.ToString("d") : "n");
        }

        private static EndType ToEndType(AssociationEndType type)
        {
            switch (type)
            {
                case AssociationEndType.Composition:
                    return EndType.Composition;
                case AssociationEndType.Aggregation:
                    return EndType.Aggregation;
                case AssociationEndType.NotNavigable:
                    return EndType.NotNavigable;
                case AssociationEndType.Navigable:
                    return EndType.Navigable;
                case AssociationEndType.None:
                    return EndType.None;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        #endregion
    }
}
