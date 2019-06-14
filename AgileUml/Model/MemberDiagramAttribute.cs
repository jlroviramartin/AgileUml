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
    /// This attribute configures if a members of a class/interface is included in
    /// a diagram.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class MemberDiagramAttribute : UmlAttribute
    {
        public MemberDiagramAttribute(params string[] diagrams)
        {
            this.Diagrams = diagrams;
        }

        /// <summary>
        /// Which diagrams this attribute are related to.
        /// </summary>
        public string[] Diagrams { get; set; }

        /// <summary>
        /// This member points out if we want to hide it from the diagrams.
        /// </summary>
        public bool Hide { get; set; }
    }
}
