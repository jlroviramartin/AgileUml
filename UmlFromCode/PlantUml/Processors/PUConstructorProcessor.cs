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
using System.Text;
using UmlFromCode.Processors;

namespace UmlFromCode.PlantUml.Processors
{
    public class PUConstructorProcessor<TPrinter> : IProcessor<ConstructorInfo, TPrinter>
        where TPrinter : IPlantUmlPrinter
    {
        public PUConstructorProcessor()
        {
        }

        public void Process(ConstructorInfo constructor, TPrinter printer)
        {
            Modifiers modifiers = Modifiers.None;

            if (constructor.IsPublic)
            {
                modifiers |= Modifiers.Public;
            }
            else if (constructor.IsProtected())
            {
                modifiers |= Modifiers.Protected;
            }
            else if (constructor.IsPrivate)
            {
                modifiers |= Modifiers.Private;
            }

            if (constructor.IsStatic)
            {
                modifiers |= Modifiers.Static;
            }
            if (constructor.IsAbstract)
            {
                modifiers |= Modifiers.Abstract;
            }
            if (constructor.IsSealed())
            {
                modifiers |= Modifiers.Sealed;
            }

            StringBuilder buff = new StringBuilder();
            foreach (ParameterInfo parameter in constructor.GetParameters())
            {
                if (buff.Length > 0)
                {
                    buff.Append(", ");
                }
                buff.AppendFormat("{0} {1}", parameter.ParameterType.GetSimpleName(), parameter.Name);
            }

            printer.PrintMethod(modifiers, null, constructor.DeclaringType.GetSimpleName(), buff.ToString());
        }
    }
}
