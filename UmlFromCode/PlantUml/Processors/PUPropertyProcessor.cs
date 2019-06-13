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
using System.Reflection;
using UmlFromCode.Processors;

namespace UmlFromCode.PlantUml.Processors
{
    public class PUPropertyProcessor<TPrinter> : IProcessor<PropertyInfo, TPrinter>
        where TPrinter : IPlantUmlPrinter
    {
        public PUPropertyProcessor()
        {
        }

        public void Process(PropertyInfo property, TPrinter printer)
        {
            Modifiers modifiers = Modifiers.None;

            if (property.IsPublic())
            {
                modifiers |= Modifiers.Public;
            }
            else if (property.IsProtected())
            {
                modifiers |= Modifiers.Protected;
            }
            else if (property.IsPrivate())
            {
                modifiers |= Modifiers.Private;
            }

            if (property.IsStatic())
            {
                modifiers |= Modifiers.Static;
            }
            if (property.IsAbstract())
            {
                modifiers |= Modifiers.Abstract;
            }
            if (property.IsSealed())
            {
                modifiers |= Modifiers.Sealed;
            }

            printer.PrintField(modifiers, property.PropertyType.GetSimpleName(), property.Name);
        }
    }
}
