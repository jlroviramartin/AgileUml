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

namespace AgileUml.Model
{
    /// <summary>
    /// This attribute configures which classes/members of a assembly/module are included in
    /// a diagram.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = true)]
    public class DiagramAttribute : UmlAttribute
    {
        public DiagramAttribute()
        {
        }

        public DiagramAttribute(params string[] diagrams)
        {
            this.Diagrams = diagrams;
        }

        /// <summary>
        /// Which diagrams this attribute are related to.
        /// </summary>
        public string[] Diagrams { get; set; } = new string[0];

        /// <summary>
        /// Which namespaces this attribute are related to.
        /// </summary>
        public string[] Namespaces { get; set; } = new string[0];

        /// <summary>
        /// Default include for namespace members.
        /// </summary>
        public NamespaceInclude DefaultNamespaceInclude { get; set; } = NamespaceInclude.None;

        /// <summary>
        /// Default include for class members.
        /// </summary>
        public ClassInclude DefaultClassInclude { get; set; } = ClassInclude.None;
    }
}
