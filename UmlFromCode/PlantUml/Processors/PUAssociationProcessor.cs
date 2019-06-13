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
using UmlFromCode.Processors;

namespace UmlFromCode.PlantUml.Processors
{
    public class PUAssociationProcessor<TPrinter> : IProcessor<Association, TPrinter>
        where TPrinter : IPlantUmlPrinter
    {
        public PUAssociationProcessor()
        {
        }

        public virtual void Process(Association association, TPrinter printer)
        {
            printer.PrintAssociation(
                association.Class1.GetSimpleName(),
                string.Format("{0} {1}", association.Member1?.Name, association.Label1),
                association.EndType1,
                association.Class2.GetSimpleName(),
                string.Format("{0} {1}", association.Member2?.Name, association.Label2),
                association.EndType2,
                association.Name, 2, LinePattern.Solid);
        }
    }
}
