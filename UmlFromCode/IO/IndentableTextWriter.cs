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
using System.Text;

namespace UmlFromCode.IO
{
    public class IndentableTextWriter : TextWriter
    {
        public IndentableTextWriter(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public string Space { get; set; } = "\t";

        public void Indent()
        {
            this.indent++;
        }

        public void Unindent()
        {
            this.indent--;
        }

        #region private

        private int indent;
        private bool doIndent;
        private readonly TextWriter textWriter;

        #endregion

        #region TextWriter

        public override Encoding Encoding
        {
            get { return this.textWriter.Encoding; }
        }

        public override void Write(char ch)
        {
            // Si hay que añadir indentacion, se añade.
            if (this.doIndent)
            {
                this.doIndent = false;
                this.textWriter.Write(this.Space.Repit(this.indent));
            }

            this.textWriter.Write(ch);

            // Si es salto de linea, se indica que el proximo caracter a escribir tiene indentacion.
            if (ch == '\n')
            {
                this.doIndent = true;
            }
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.textWriter.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
