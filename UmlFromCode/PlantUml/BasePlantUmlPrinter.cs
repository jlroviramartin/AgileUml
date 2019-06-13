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
using System.IO;
using UmlFromCode.IO;

namespace UmlFromCode.PlantUml
{
    public abstract class BasePlantUmlPrinter : IPlantUmlPrinter
    {
        public BasePlantUmlPrinter()
        {
        }

        public BasePlantUmlPrinter(TextWriter textWriter, bool dispose = true)
            : this(new TextPrinter(textWriter, dispose))
        {
        }

        public BasePlantUmlPrinter(ITextPrinter printer)
        {
            this.printer = printer;
        }

        protected ITextPrinter printer;

        #region private

        /// <summary>Checked objects in the current diagram.</summary>
        private HashSet<object> @checked = new HashSet<object>();

        #endregion

        #region IPlantUmlPrinter

        public string Diagram { get; private set; }

        public string Class { get; private set; }

        public virtual void BeginDiagram(string name)
        {
            this.Diagram = name;
            this.@checked.Clear();

            this.printer.PrintFormatLn("@startuml {0}", name);
            this.printer.Indent();
        }

        public virtual void EndDiagram()
        {
            this.printer.Unindent();
            this.printer.PrintLn("@enduml");

            this.Diagram = null;
            this.@checked.Clear();
        }

        public virtual void BeginClass(ClassType classType, string name, string generics = null, string[] stereotypes = null)
        {
            stereotypes = stereotypes ?? new string[0];

            this.Class = name;

            string keyword;
            switch (classType)
            {
                case ClassType.Class:
                    keyword = "class";
                    break;
                case ClassType.Interface:
                    keyword = "interface";
                    break;
                case ClassType.Enum:
                    keyword = "enum";
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            this.printer.PrintFormat("{0} {1}", keyword, name);
            if (generics != null)
            {
                this.printer.PrintFormat(" <{0}>", generics);
            }
            if (stereotypes.Length > 0)
            {
                foreach (string stereotype in stereotypes)
                {
                    this.printer.PrintFormat(" <<{0}>>", stereotype);
                }
            }
            this.printer.PrintLn();
            this.printer.PrintLn("{");
            this.printer.Indent();
        }

        public virtual void EndClass()
        {
            this.printer.Unindent();
            this.printer.PrintLn("}");
            this.Class = null;
        }

        public virtual void PrintConstructor(Modifiers modifiers, string type, string arguments)
        {
            this.printer.PrintFormat("{{method}} {0} {1} {2} ({3})", modifiers.GetSymbol(), type, arguments).PrintLn();
        }

        public virtual void PrintMethod(Modifiers modifiers, string type, string name, string arguments)
        {
            this.printer.PrintFormat("{{method}} {0} {1} {2} ({3})", modifiers.GetSymbol(), type, name, arguments).PrintLn();
        }

        public virtual void PrintField(Modifiers modifiers, string type, string name)
        {
            this.printer.PrintFormat("{{field}} {0} {1} {2}", modifiers.GetSymbol(), type, name).PrintLn();
        }

        public virtual void PrintAssociation(string type1, string label1, EndType endType1,
                                             string type2, string label2, EndType endType2,
                                             string name, int size = 2, LinePattern pattern = LinePattern.Solid)
        {
            this.printer.Print(type1);

            if (!string.IsNullOrEmpty(label1))
            {
                this.printer.PrintFormat(" \"{0}\"", label1);
            }

            this.printer.PrintFormat(" {0}{1}{2}",
                                     endType1.GetLeftSymbol(),
                                     pattern.GetSymbol().Repit(size),
                                     endType2.GetRightSymbol());

            if (!string.IsNullOrEmpty(label2))
            {
                this.printer.PrintFormat(" \"{0}\"", label2);
            }

            this.printer.PrintFormat(" {0}", type2);

            if (!string.IsNullOrEmpty(name))
            {
                this.printer.PrintFormat(" : {0}", name);
            }
            this.printer.PrintLn();
        }

        public virtual void PrintGeneralization(string general, string specific,
                                                string name, int size = 2, LinePattern pattern = LinePattern.Solid)
        {
            this.printer.Print(general);

            this.printer.PrintFormat(" {0}{1}", "<|", pattern.GetSymbol().Repit(size));

            this.printer.PrintFormat(" {0}", specific);

            if (!string.IsNullOrEmpty(name))
            {
                this.printer.PrintFormat(" : {0}", name);
            }
            this.printer.PrintLn();
        }

        public void CheckInDiagram(object obj)
        {
            this.@checked.Add(obj);
        }

        public bool IsCheckedInDiagram(object obj)
        {
            return this.@checked.Contains(obj);
        }

        #endregion
    }
}
