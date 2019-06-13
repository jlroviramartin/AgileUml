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
    /// Class members to include.
    /// </summary>
    [Flags]
    public enum ClassInclude
    {
        None = 0,

        All = Constructors
            | Methods
            | Properties
            | Fields
            | Generalizations
            | Associations
            | Dependencies,

        Constructors = 1,
        Methods = 2,
        Properties = 4,
        Fields = 8,
        Generalizations = 16,
        Associations = 32,
        Dependencies = 64,
    }
}
