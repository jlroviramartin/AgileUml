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
using UmlFromCode.PlantUml.Selectors;
using UmlFromCode.Processors;

namespace UmlFromCode.PlantUml.Processors
{
    public class PUClassProcessor<TPrinter> : IProcessor<Type, TPrinter>
        where TPrinter : IPlantUmlPrinter
    {
        public PUClassProcessor(IProcessorProvider<TPrinter> provider)
        {
            this.provider = provider;
        }

        #region private

        private void ProcessMembersIfChecked<T>(ClassSelector classSelector, IEnumerable<T> members, TPrinter printer)
        {
            foreach (T member in members)
            {
                if (classSelector.Check(member, printer.Diagram))
                {
                    this.provider.GetProcessor<T>().Process(member, printer);
                }
            }
        }

        private void ProcessNestedIfChecked(ClassSelector classSelector, IEnumerable<Type> nestedTypes, TPrinter printer)
        {
            foreach (Type nestedType in nestedTypes)
            {
                if (classSelector.CheckNested(nestedType, printer.Diagram))
                {
                    this.provider.GetProcessor<Type>().Process(nestedType, printer);
                }
            }
        }

        private readonly IProcessorProvider<TPrinter> provider;

        #endregion

        #region IProcessor<Type, TPrinter>

        public virtual void Process(Type @class, TPrinter printer)
        {
            // NOTE: CHANGE IT!!!
            AssemblySelector selector = AssemblySelector.Get(@class.Assembly);

            ClassInclude defaultClassInclude = selector.GetClassInclude(@class, printer.Diagram);
            ClassSelector classSelector = ClassSelector.Get(@class, defaultClassInclude);

            printer.BeginClass(ModelUtils.ToClassType(@class),
                               PlantUmlUtils.GetSimpleName(@class),
                               PlantUmlUtils.GetGenerics(@class),
                               PlantUmlUtils.GetStereotypes(@class)?.ToArray());

            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetConstructors(@class), printer);
            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetMethods(@class), printer);
            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetProperties(@class), printer);
            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetEvents(@class), printer);
            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetFields(@class), printer);

            printer.EndClass();

            //this.ProcessNestedIfChecked(classSelector, ModelUtils.GetNestedTypes(@class), printer);

            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetGeneralizations(@class), printer);
            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetAssociations(@class), printer);
            this.ProcessMembersIfChecked(classSelector, ModelUtils.GetDependencies(@class), printer);
        }

        #endregion
    }
}
