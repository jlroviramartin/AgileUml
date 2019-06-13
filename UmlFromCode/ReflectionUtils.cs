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
using System.Runtime.CompilerServices;
using System.Text;

namespace UmlFromCode
{
    public static class ReflectionUtils
    {
        // NOTE: find better way of doing this.
        /// <summary>
        /// Decompose the flag into atomic enum values of 1 bit.
        /// </summary>
        public static IEnumerable<T> DecomposeFlag<T>(this T flags) where T : Enum
        {
            ulong f = ((IConvertible) flags).ToUInt64(null);
            bool Check(ulong current)
            {
                return (f & current) == current;
            }

            foreach (ulong flag in Enum.GetValues(typeof(T)).Cast<T>().Select(x => ((IConvertible) x).ToUInt64(null)))
            {
                if ((Utils.NumberOfSetBits(flag) == 1) && Check(flag))
                {
                    yield return (T) Enum.ToObject(typeof(T), flag);
                }
            }
        }

        public static bool IsCompilerGenerated(this MemberInfo member)
        {
            return member.IsDefined(typeof(CompilerGeneratedAttribute), false);
        }

        public static bool IsDelegate(this Type type)
        {
            return typeof(Delegate).IsAssignableFrom(type);
        }

        public static bool IsPublic(this EventInfo @event)
        {
            MethodInfo method = @event.GetAddMethod() ?? @event.GetRemoveMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsPublic;
        }

        public static bool IsProtected(this EventInfo @event)
        {
            MethodInfo method = @event.GetAddMethod() ?? @event.GetRemoveMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsFamily || method.IsFamilyAndAssembly || method.IsFamilyOrAssembly;
        }

        public static bool IsPrivate(this EventInfo @event)
        {
            MethodInfo method = @event.GetAddMethod() ?? @event.GetRemoveMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsPrivate;
        }

        public static bool IsStatic(this EventInfo @event)
        {
            MethodInfo method = @event.GetAddMethod() ?? @event.GetRemoveMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsStatic;
        }

        public static bool IsAbstract(this EventInfo @event)
        {
            MethodInfo method = @event.GetAddMethod() ?? @event.GetRemoveMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsAbstract;
        }

        public static bool IsSealed(this EventInfo @event)
        {
            MethodInfo method = @event.GetAddMethod() ?? @event.GetRemoveMethod();
            if (method == null)
            {
                return false;
            }
            return !method.IsVirtual || method.IsFinal;
        }

        public static bool IsPublic(this PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod() ?? property.GetSetMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsPublic;
        }

        public static bool IsProtected(this PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod() ?? property.GetSetMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsFamily || method.IsFamilyAndAssembly || method.IsFamilyOrAssembly;
        }

        public static bool IsPrivate(this PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod() ?? property.GetSetMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsPrivate;
        }

        public static bool IsStatic(this PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod() ?? property.GetSetMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsStatic;
        }

        public static bool IsAbstract(this PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod() ?? property.GetSetMethod();
            if (method == null)
            {
                return false;
            }
            return method.IsAbstract;
        }

        public static bool IsSealed(this PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod() ?? property.GetSetMethod();
            if (method == null)
            {
                return false;
            }
            return !method.IsVirtual || method.IsFinal;
        }

        public static bool IsProtected(this MethodBase method)
        {
            return method.IsFamily || method.IsFamilyAndAssembly || method.IsFamilyOrAssembly;
        }

        public static bool IsSealed(this MethodBase method)
        {
            return !method.IsVirtual || method.IsFinal;
        }

        public static bool IsProtected(this FieldInfo field)
        {
            return field.IsFamily || field.IsFamilyAndAssembly || field.IsFamilyOrAssembly;
        }

        /// <summary>
        /// This method returns the interfaces that the class directly implements.
        /// For example:
        /// <code><![CDATA[
        /// IB <-  D <- C
        /// IA <- IB <- C
        ///       IC <- C
        /// ]]></code>
        /// C implements IA, IB and IC and extends C, but this method returns only IC
        /// because IA is implemented by IB and IC is implemented by D.
        /// </summary>
        public static IEnumerable<Type> GetDirectInterfaces(this Type @class)
        {
            HashSet<Type> interfaces = new HashSet<Type>(@class.GetInterfaces());

            if (@class.BaseType != null)
            {
                interfaces.ExceptWith(@class.BaseType.GetInterfaces());
            }

            foreach (Type @interface in interfaces.ToArray())
            {
                interfaces.ExceptWith(@interface.GetInterfaces());
            }

            return interfaces;
        }
    }
}
