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
    /// Attribute that points out a member of a class and relates it with other member in an
    /// associated class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AssociationEndAttribute : UmlAttribute
    {
        public AssociationEndAttribute()
        {
        }

        public AssociationEndAttribute(Type otherEnd, string otherProperty, AssociationEndType type = AssociationEndType.None, int[] cardinality = null)
        {
            this.OtherEnd = otherEnd;
            this.OtherProperty = otherProperty;
            this.Type = type;
            this.Cardinality = cardinality;
        }

        public AssociationEndAttribute(Type otherEnd, string otherProperty, int[] cardinality)
        {
            this.OtherEnd = otherEnd;
            this.OtherProperty = otherProperty;
            this.Cardinality = cardinality;
        }

        /// <summary>The name of the association.</summary>
        public string Name { get; set; }

        /// <summary>The other end of the association.</summary>
        public Type OtherEnd { get; set; }

        /// <summary>The name of the property on the <code>otherEnd</code> class, related to
        /// this one.</summary>
        public string OtherProperty { get; set; }

        /// <summary>The type of association end.</summary>
        public AssociationEndType Type { get; set; } = AssociationEndType.None;

        /// <summary>The cardinality of association end.</summary>
        public int[] Cardinality { get; set; }
    }
}
