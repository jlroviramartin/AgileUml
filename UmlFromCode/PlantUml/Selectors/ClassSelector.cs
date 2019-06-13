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
using AgileUml.Model;
using UmlFromCode.Selectors;

namespace UmlFromCode.PlantUml.Selectors
{
    /// <summary>
    /// This class groups all <code>ClassDiagramAttribute</code> of a type.
    /// </summary>
    public class ClassSelector
    {
        public static ClassSelector Get(Type @class, ClassInclude defaultClassInclude = ClassInclude.None)
        {
            ClassSelector classSelector = new ClassSelector(defaultClassInclude);
            classSelector.AddRange(@class.GetCustomAttributes(false).OfType<ClassDiagramAttribute>());
            return classSelector;
        }

        /// <summary>
        /// Checks if the <code>member</code> is allowed for the <code>diagram</code>.
        /// </summary>
        public bool Check(object member, string diagram)
        {
            // Checks if the 'member' is allowed.
            return GetClassInclude(diagram).Check(member);
        }

        /// <summary>
        /// Checks if the <code>member</code> is allowed for the <code>diagram</code>.
        /// </summary>
        public bool CheckNested(Type type, string diagram)
        {
            // Checks if the 'type' is allowed.
            return GetNestedInclude(diagram).Check(type);
        }

        public NamespaceInclude GetNestedInclude(string diagram)
        {
            // Checks the 'diagram' against the 'selector'.
            bool CheckDiagram(Selector selector)
            {
                return selector.DiagramSelector.Select(diagram);
            }

            // Filtered selectors.
            IEnumerable<Selector> filtered = this.selectors
                .Where(CheckDiagram);

            // Concatenates all namespace permissions for the 'diagram'.
            NamespaceInclude namespaceInclude = filtered.Aggregate(NamespaceInclude.None, (acc, selector) => acc | selector.NestedInclude);

            return namespaceInclude;
        }

        public ClassInclude GetClassInclude(string diagram)
        {
            // Checks the 'diagram' against the 'selector'.
            bool CheckDiagram(Selector selector)
            {
                return selector.DiagramSelector.Select(diagram);
            }

            // Filtered selectors (cache).
            IEnumerable<Selector> filtered = this.selectors
                .Where(CheckDiagram)
                .ToArray();

            // If there is no selector, it uses the default behaviour.
            if (!filtered.Any())
            {
                return this.defaultClassInclude;
            }

            // Concatenates all class permissions for the 'diagram'.
            ClassInclude classInclude = filtered.Aggregate(ClassInclude.None, (acc, selector) => acc | selector.Include);
            return classInclude;
        }

        #region private

        private ClassSelector(ClassInclude defaultClassInclude = ClassInclude.None)
        {
            this.defaultClassInclude = defaultClassInclude;
        }

        private void Add(ClassDiagramAttribute attr)
        {
            ISelector<string> diagramSelector = SelectorUtils.BuildSelector(attr.Diagrams);
            this.selectors.Add(
                new Selector()
                {
                    DiagramSelector = diagramSelector,
                    NestedInclude = attr.NestedInclude,
                    Include = attr.Include
                });
        }

        private void AddRange(IEnumerable<ClassDiagramAttribute> attrs)
        {
            foreach (ClassDiagramAttribute attr in attrs)
            {
                this.Add(attr);
            }
        }

        private readonly ClassInclude defaultClassInclude;
        private readonly List<Selector> selectors = new List<Selector>();

        private class Selector
        {
            public ISelector<string> DiagramSelector { get; set; }

            public NamespaceInclude NestedInclude { get; set; } = NamespaceInclude.None;

            public ClassInclude Include { get; set; } = ClassInclude.None;
        }

        #endregion
    }
}
