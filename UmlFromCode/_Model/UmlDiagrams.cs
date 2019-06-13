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
using AgileUml.Model;

[assembly: Diagram(
    Diagrams = new string[] { "Inheritance" },
    Namespaces = new string[] { "UmlFromCode", "UmlFromCode.**" },
    DefaultNamespaceInclude = NamespaceInclude.All,
    DefaultClassInclude = ClassInclude.Generalizations)]

[assembly: Diagram(
    Diagrams = new string[] { "Relationships" },
    Namespaces = new string[] { "UmlFromCode", "UmlFromCode.**" },
    DefaultNamespaceInclude = NamespaceInclude.All,
    DefaultClassInclude = ClassInclude.Constructors
        | ClassInclude.Methods
        | ClassInclude.Properties
        | ClassInclude.Fields
        | ClassInclude.Associations
        | ClassInclude.Dependencies)]
