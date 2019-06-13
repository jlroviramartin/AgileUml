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

        public string Name { get; set; }

        public Type OtherEnd { get; set; }

        public string OtherProperty { get; set; }

        public AssociationEndType Type { get; set; } = AssociationEndType.None;

        public int[] Cardinality { get; set; }
    }
}
