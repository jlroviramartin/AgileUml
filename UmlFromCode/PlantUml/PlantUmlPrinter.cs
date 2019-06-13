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
using System.IO;
using UmlFromCode.IO;

namespace UmlFromCode.PlantUml
{
    public class PlantUmlPrinter : BasePlantUmlPrinter
    {
        public PlantUmlPrinter(string path)
        {
            this.path = path;
        }

        #region private

        private readonly string path;

        #endregion

        #region IPlantUmlPrinter

        public override void BeginDiagram(string name)
        {
            string current = Path.Combine(this.path, name + ".puml");

            StreamWriter writer = new StreamWriter(new BufferedStream(new FileStream(current, FileMode.Create, FileAccess.Write)));
            this.printer = new TextPrinter(writer, true);

            base.BeginDiagram(name);
        }

        public override void EndDiagram()
        {
            base.EndDiagram();

            this.printer.Dispose();
            this.printer = null;
        }

        #endregion
    }
}
