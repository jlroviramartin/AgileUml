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
using UmlFromCode.Selectors;

namespace UmlFromCode.PlantUml.Selectors
{
    /// <summary>
    /// This class groups all <code>DiagramAttribute</code> of an assembly.
    /// </summary>
    public class AssemblySelector
    {
        public static AssemblySelector Get(Assembly assembly)
        {
            AssemblySelector assemblySelector = new AssemblySelector();
            assemblySelector.AddRange(assembly.GetCustomAttributes<DiagramAttribute>());
            return assemblySelector;
        }

        public IEnumerable<string> GetDiagrams()
        {
            return this.selectors.SelectMany(selector => selector.Diagrams).ToHashSet();
        }

        /// <summary>
        /// Checks if the <code>type</code> is allowed for the <code>diagram</code>.
        /// </summary>
        public bool Check(Type type, string diagram)
        {
            return this.GetNamespaceInclude(type, diagram).Check(type);
        }

        public NamespaceInclude GetNamespaceInclude(Type type, string diagram)
        {
            // Checks the namespace of 'type' and the 'diagram' against the 'selector'.
            bool CheckNamespaceAndDiagram(Selector selector)
            {
                return selector.NamespaceSelector.Select(type.Namespace)
                    && selector.DiagramSelector.Select(diagram);
            }

            // Filtered selectors.
            IEnumerable<Selector> filtered = this.selectors
                .Where(CheckNamespaceAndDiagram);

            // Concatenates all namespace permissions related to the namespace of 'type' for the 'diagram'.
            NamespaceInclude namespaceInclude = filtered.Aggregate(NamespaceInclude.None, (acc, selector) => acc | selector.DefaultNamespaceInclude);

            return namespaceInclude;
        }

        public ClassInclude GetClassInclude(Type type, string diagram)
        {
            // Checks the namespace of 'type' and the 'diagram' against the 'selector'.
            bool CheckNamespaceAndDiagram(Selector selector)
            {
                return selector.NamespaceSelector.Select(type.Namespace)
                    && selector.DiagramSelector.Select(diagram);
            }

            // Filtered selectors.
            IEnumerable<Selector> filtered = this.selectors
                .Where(CheckNamespaceAndDiagram);

            // Concatenates all class permissions related to the namespace of 'type' for the 'diagram'.
            ClassInclude classInclude = filtered.Aggregate(ClassInclude.None, (acc, selector) => acc | selector.DefaultClassInclude);

            return classInclude;
        }

        #region private

        private AssemblySelector()
        {
        }

        private void Add(DiagramAttribute attr)
        {
            ISelector<string> diagramSelector = SelectorUtils.BuildSelector(attr.Diagrams);
            ISelector<string> namespaceSelector = SelectorUtils.BuildSelector(attr.Namespaces);
            this.selectors.Add(
                new Selector()
                {
                    Diagrams = SelectorUtils.GetFixed(attr.Diagrams).ToList(),
                    DiagramSelector = diagramSelector,
                    NamespaceSelector = namespaceSelector,
                    DefaultNamespaceInclude = attr.DefaultNamespaceInclude,
                    DefaultClassInclude = attr.DefaultClassInclude
                });
        }

        private void AddRange(IEnumerable<DiagramAttribute> attrs)
        {
            foreach (DiagramAttribute attr in attrs)
            {
                this.Add(attr);
            }
        }

        private readonly List<Selector> selectors = new List<Selector>();

        private class Selector
        {
            public IEnumerable<string> Diagrams { get; set; }

            public ISelector<string> DiagramSelector { get; set; }

            public ISelector<string> NamespaceSelector { get; set; }

            public NamespaceInclude DefaultNamespaceInclude { get; set; } = NamespaceInclude.None;

            public ClassInclude DefaultClassInclude { get; set; } = ClassInclude.None;
        }

        #endregion
    }
}
