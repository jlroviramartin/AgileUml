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
using System.Linq;
using System.Reflection;
using AgileUml.Model;
using UmlFromCode.PlantUml.Selectors;
using UmlFromCode.Processors;

namespace UmlFromCode.PlantUml.Processors
{
    public class PUAssemblyProcessor<TPrinter> : IProcessor<Assembly, TPrinter>
        where TPrinter : IPlantUmlPrinter
    {
        public PUAssemblyProcessor(IProcessorProvider<TPrinter> provider)
        {
            this.provider = provider;
        }

        #region private

        private readonly IProcessorProvider<TPrinter> provider;

        #endregion

        #region IProcessor<Assembly, TPrinter>

        public virtual void Process(Assembly assembly, TPrinter printer)
        {
            AssemblySelector selector = AssemblySelector.Get(assembly);

            // Check in which diagram can be used.
            foreach (string diagram in selector.GetDiagrams())
            {
                printer.BeginDiagram(diagram);

                foreach (Type @class in assembly.DefinedTypes.Where(@class => !@class.IsCompilerGenerated()))
                {
                    ClassSelector classSelector = ClassSelector.Get(@class);

                    // Is pointed out with a 'DiagramAttribute' OR 'ClassDiagramAttribute'.
                    if (selector.Check(@class, diagram)
                        || classSelector.GetClassInclude(diagram) != ClassInclude.None)
                    {
                        this.provider.GetProcessor<Type>().Process(@class, printer);
                    }
                }

                printer.EndDiagram();
            }
        }

        #endregion
    }
}
