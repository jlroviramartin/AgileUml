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

namespace UmlFromCode.IO
{
    public static class TextPrinterUtils
    {
        public static ITextPrinter PrintLn(this ITextPrinter printer)
        {
            return printer.Print(Environment.NewLine);
        }

        public static ITextPrinter PrintLn(this ITextPrinter printer, string text)
        {
            return printer.Print(text).Print(Environment.NewLine);
        }

        public static ITextPrinter PrintFormat(this ITextPrinter printer, string format, params object[] args)
        {
            return printer.Print(string.Format(format, args));
        }

        public static ITextPrinter PrintFormatLn(this ITextPrinter printer, string format, params object[] args)
        {
            return printer.Print(string.Format(format, args)).Print(Environment.NewLine);
        }
    }
}
