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
    /// Namespace members to include.
    /// </summary>
    [Flags]
    public enum NamespaceInclude
    {
        None = 0,

        All = Classes
            | Interfaces
            | Structures
            | Enums
            | Delegates,

        Classes = 1,
        Interfaces = 2,
        Structures = 4,
        Enums = 8,
        Delegates = 16,
    }
}
