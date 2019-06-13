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
using AgileUml.Model;
using UmlFromCode.Processors;

namespace UmlFromCode.PlantUml.Processors
{
    [Dependency(typeof(PUAssemblyProcessor<>), "uses")]
    [Dependency(typeof(PUClassProcessor<>), "uses")]
    [Dependency(typeof(PUConstructorProcessor<>), "uses")]
    [Dependency(typeof(PUMethodProcessor<>), "uses")]
    [Dependency(typeof(PUPropertyProcessor<>), "uses")]
    [Dependency(typeof(PUFieldProcessor<>), "uses")]
    [Dependency(typeof(PUEventProcessor<>), "uses")]
    [Dependency(typeof(PUGeneralizationProcessor<>), "uses")]
    [Dependency(typeof(PUAssociationProcessor<>), "uses")]
    [Dependency(typeof(PUDependencyProcessor<>), "uses")]
    [Dependency(typeof(ProcessorProvider<>), "uses")]
    public static class PUProcessorUtils
    {
        /// <summary>
        /// This method creates a processor that checks <code>IPlantUmlPrinter</code> if the
        /// object is already printed.
        /// </summary>
        public static IProcessor<T, TPrinter> CheckInDiagram<T, TPrinter>(this IProcessor<T, TPrinter> processor)
            where T : class
            where TPrinter : IPlantUmlPrinter
        {
            return new PUCheckInDiagramProcessor<T, TPrinter>(processor);
        }

        /// <summary>
        /// This method configures a default processor.
        /// </summary>
        public static IProcessor<Assembly, TPrinter> Configure<TPrinter>()
            where TPrinter : IPlantUmlPrinter
        {
            ProcessorProvider<TPrinter> provider = new ProcessorProvider<TPrinter>();

            provider.Register(new PUAssemblyProcessor<TPrinter>(provider).CheckInDiagram());
            provider.Register(new PUClassProcessor<TPrinter>(provider).CheckInDiagram());
            provider.Register(new PUConstructorProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUMethodProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUPropertyProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUFieldProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUEventProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUGeneralizationProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUAssociationProcessor<TPrinter>().CheckInDiagram());
            provider.Register(new PUDependencyProcessor<TPrinter>().CheckInDiagram());

            return provider.GetProcessor<Assembly>();
        }
    }
}
