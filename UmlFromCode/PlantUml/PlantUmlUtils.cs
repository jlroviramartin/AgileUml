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
using System.Text;

namespace UmlFromCode.PlantUml
{
    public static class PlantUmlUtils
    {
        public static string GetSymbol(this Modifiers flags)
        {
            StringBuilder buff = new StringBuilder();
            foreach (Modifiers flag in flags.DecomposeFlag())
            {
                if (mapModifiers.TryGetValue(flag, out string text))
                {
                    buff.Append(text);
                }
            }
            return buff.ToString();
        }

        public static string GetSymbol(this LinePattern pattern)
        {
            switch (pattern)
            {
                case LinePattern.Solid:
                    return "-";
                case LinePattern.Dotted:
                    return ".";
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public static string GetLeftSymbol(this EndType endType)
        {
            if (mapEndType2LeftRight.TryGetValue(endType, out Tuple<string, string> leftRight))
            {
                return leftRight.Item1;
            }
            return "";
        }

        public static string GetRightSymbol(this EndType endType)
        {
            if (mapEndType2LeftRight.TryGetValue(endType, out Tuple<string, string> leftRight))
            {
                return leftRight.Item2;
            }
            return "";
        }

        public static string GetSimpleName(this Type @class)
        {
            string name;
            if (@class.IsPrimitive && mapPrimitive.TryGetValue(@class, out name))
            {
                return name;
            }

            name = @class.Name;
            if (@class.IsGenericType)
            {
                int index = name.IndexOf('`');
                if (index >= 0)
                {
                    return name.Substring(0, index);
                }

                if (@class.IsGenericTypeDefinition)
                {
                    // It is a class
                }
                else
                {
                }
            }
            return name;
        }

        public static string GetGenerics(this Type @class)
        {
            string generics = null;
            if (@class.IsGenericType)
            {
                if (@class.IsGenericTypeDefinition)
                {
                    // It is a class
                    StringBuilder buff = new StringBuilder();
                    foreach (Type parameter in @class.GetGenericArguments())
                    {
                        if (buff.Length > 0)
                        {
                            buff.Append(", ");
                        }
                        buff.Append(parameter.Name);
                        //Type[] genericParameterConstraints = parameter.GetGenericParameterConstraints();
                    }
                    generics = buff.ToString();
                }
                else
                {
                    // It is a class
                    foreach (Type parameter in @class.GetGenericArguments())
                    {
                        Type[] genericParameterConstraints = parameter.GetGenericParameterConstraints();
                    }
                }
            }
            return generics;
        }

        public static IEnumerable<string> GetStereotypes(this Type @class)
        {
            return null;
        }

        #region private

        static PlantUmlUtils()
        {
            mapModifiers.Add(Modifiers.Public, "+");
            mapModifiers.Add(Modifiers.Protected, "#");
            mapModifiers.Add(Modifiers.Private, "-");
            mapModifiers.Add(Modifiers.Package, "~");

            mapModifiers.Add(Modifiers.Static, "{static}");
            mapModifiers.Add(Modifiers.Abstract, "{abstract}");
            mapModifiers.Add(Modifiers.Sealed, "");

            //AddLeftRight(EndType.Extension, "<|", "|>");
            AddLeftRight(EndType.Nested, "+", "+");
            AddLeftRight(EndType.Composition, "*", "*");
            AddLeftRight(EndType.Aggregation, "o", "o");
            AddLeftRight(EndType.NotNavigable, "x", "x");
            AddLeftRight(EndType.Navigable, "<", ">");

            AddPrimitive<sbyte>("sbyte");
            AddPrimitive<short>("short");
            AddPrimitive<int>("int");
            AddPrimitive<long>("long");

            AddPrimitive<byte>("byte");
            AddPrimitive<ushort>("ushort");
            AddPrimitive<uint>("uint");
            AddPrimitive<ulong>("ulong");

            AddPrimitive<float>("float");
            AddPrimitive<double>("double");
            AddPrimitive<decimal>("decimal");
        }

        private static void AddLeftRight(EndType endType, string left, string right)
        {
            mapEndType2LeftRight.Add(endType, Tuple.Create(left, right));
        }

        private static void AddPrimitive<T>(string name)
        {
            mapPrimitive.Add(typeof(T), name);
        }

        private static Dictionary<EndType, Tuple<string, string>> mapEndType2LeftRight = new Dictionary<EndType, Tuple<string, string>>();
        private static Dictionary<Modifiers, string> mapModifiers = new Dictionary<Modifiers, string>();

        private static Dictionary<Type, string> mapPrimitive = new Dictionary<Type, string>();

        #endregion
    }
}
