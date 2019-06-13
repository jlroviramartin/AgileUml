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
    public static class IncludeUtils
    {
        public static bool Check(this NamespaceInclude flags, Type type)
        {
            return flags.DecomposeFlag().Any(flag => fastNamespaceCheck[flag](type));
        }

        public static bool Check(this ClassInclude flags, object o)
        {
            return flags.DecomposeFlag().Any(flag => fastMemberCheck[flag](o));
        }

        #region private

        static IncludeUtils()
        {
            fastNamespaceCheck.Add(NamespaceInclude.Classes, t => t.IsClass && !t.IsDelegate());
            fastNamespaceCheck.Add(NamespaceInclude.Interfaces, t => t.IsInterface);
            fastNamespaceCheck.Add(NamespaceInclude.Structures, t => t.IsValueType && !t.IsEnum);
            fastNamespaceCheck.Add(NamespaceInclude.Enums, t => t.IsEnum);
            fastNamespaceCheck.Add(NamespaceInclude.Delegates, t => t.IsDelegate());

            fastMemberCheck.Add(ClassInclude.Constructors, o => o is ConstructorInfo);
            fastMemberCheck.Add(ClassInclude.Methods, o => o is MethodInfo);
            fastMemberCheck.Add(ClassInclude.Properties, o => o is PropertyInfo);
            fastMemberCheck.Add(ClassInclude.Fields, o => o is FieldInfo);
            fastMemberCheck.Add(ClassInclude.Generalizations, o => o is Generalization);
            fastMemberCheck.Add(ClassInclude.Associations, o => o is Association);
            fastMemberCheck.Add(ClassInclude.Dependencies, o => o is Dependency);
        }

        private static Dictionary<NamespaceInclude, Func<Type, bool>> fastNamespaceCheck = new Dictionary<NamespaceInclude, Func<Type, bool>>();
        private static Dictionary<ClassInclude, Func<object, bool>> fastMemberCheck = new Dictionary<ClassInclude, Func<object, bool>>();

        #endregion
    }
}
