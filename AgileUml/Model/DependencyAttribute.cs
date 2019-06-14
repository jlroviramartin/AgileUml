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
using System.Diagnostics.Contracts;

namespace AgileUml.Model
{
    /// <summary>
    /// Attribute that documents a dependency to another class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute()
        {
        }

        public DependencyAttribute(Type supplier, string name = null)
        {
            Contract.Assert(supplier != null);

            this.Supplier = supplier;
            this.Name = name;
        }

        /// <summary>The supplier of the dependency.</summary>
        public Type Supplier { get; set; }

        /// <summary>The name of the dependency.</summary>
        public string Name { get; set; }
    }
}
