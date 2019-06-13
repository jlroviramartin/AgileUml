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
    public class PUCheckInDiagramProcessor<T, TPrinter> : IProcessor<T, TPrinter>
        where T : class
        where TPrinter : IPlantUmlPrinter
    {
        public PUCheckInDiagramProcessor(IProcessor<T, TPrinter> inner)
        {
            this.inner = inner;
        }

        #region private

        private readonly IProcessor<T, TPrinter> inner;

        #endregion

        #region IProcessor<T, TPrinter>

        public void Process(T item, TPrinter printer)
        {
            if (!printer.IsCheckedInDiagram(item))
            {
                printer.CheckInDiagram(item);

                this.inner.Process(item, printer);
            }
        }

        #endregion
    }
}
