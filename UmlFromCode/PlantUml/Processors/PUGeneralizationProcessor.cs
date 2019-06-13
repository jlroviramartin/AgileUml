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
    public class PUGeneralizationProcessor<TPrinter> : IProcessor<Generalization, TPrinter>
        where TPrinter : IPlantUmlPrinter
    {
        public PUGeneralizationProcessor()
        {
        }

        public virtual void Process(Generalization generalization, TPrinter printer)
        {
            LinePattern pattern = LinePattern.Solid;
            if (generalization.General.IsInterface && generalization.Specific.IsClass)
            {
                pattern = LinePattern.Dotted;
            }
            printer.PrintGeneralization(
                PlantUmlUtils.GetSimpleName(generalization.General),
                PlantUmlUtils.GetSimpleName(generalization.Specific),
                generalization.Name, 2, pattern);
        }
    }
}
