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
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UmlFromCode.PlantUml;
using UmlFromCode.PlantUml.Processors;
using UmlFromCode.Processors;

namespace UmlFromCode
{
    public class Program
    {
        public static async Task<int> Main(params string[] args)
        {
            RootCommand rootCommand = new RootCommand(
                description: "Generates PlanUml images from code, using the AgileUml module.",
                treatUnmatchedTokensAsErrors: true);

            Option inputOption = new Option(
                aliases: new string[] { "--input", "-i" },
                description: "The path to the dll file that is to be documented.",
                argument: new Argument<FileInfo>());
            rootCommand.AddOption(inputOption);

            Option outputOption = new Option(
                aliases: new string[] { "--output", "-o" },
                description: "The target output path where the images are stored.",
                argument: new Argument<DirectoryInfo>());
            rootCommand.AddOption(outputOption);

            rootCommand.Handler = CommandHandler.Create<FileInfo, DirectoryInfo>(Run);

            return await rootCommand.InvokeAsync(args);
        }

        private static void Run(FileInfo input, DirectoryInfo output)
        {
            if (!input.Exists)
            {
                Console.WriteLine("The file does not exist");
                return;
            }

            if (!output.Exists)
            {
                Console.WriteLine("The output does not exist");
                return;
            }

            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFile(input.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception loading the dll: " + e);
                return;
            }

            IProcessor<Assembly, IPlantUmlPrinter> processor = PUProcessorUtils.Configure<IPlantUmlPrinter>();
            processor.Process(typeof(Program).Assembly, new PlantUmlPrinter(output.FullName));
        }
    }
}
