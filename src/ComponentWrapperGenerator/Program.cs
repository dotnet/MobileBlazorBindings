using System;
using System.IO;

namespace ComponentWrapperGenerator
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        internal static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run FileWithOneTypePerLine.txt ..\\OutputFolder");
                Console.WriteLine("    FileWithOneTypePerLine.txt must list types in the Xamarin.Forms namespace in the Xamarin.Forms assembly.");
                return -1;
            }

            var listOfTypeToGenerate = File.ReadAllLines(args[0]);
            var outputFolder = args[1];

            var settings = new GeneratorSettings
            {
                FileHeader = @"// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
",
                RootNamespace = "Microsoft.MobileBlazorBindings.Elements",
            };

            foreach (var typeToGenerate in listOfTypeToGenerate)
            {
                if (string.IsNullOrWhiteSpace(typeToGenerate))
                {
                    continue;
                }
                if (typeToGenerate[0] == '#')
                {
                    Console.WriteLine($"Skipping line: {typeToGenerate}");
                    Console.WriteLine();
                    continue;
                }

                GenerateWrapperForType(typeToGenerate, settings, outputFolder);
            }

            return 0;
        }

        private static void GenerateWrapperForType(string typeName, GeneratorSettings settings, string outputFolder)
        {
            var fullTypeName = "Xamarin.Forms." + typeName;
            var typeToGenerate = typeof(Xamarin.Forms.Element).Assembly.GetType(fullTypeName);
            if (typeToGenerate == null)
            {
                Console.WriteLine($"WARNING: Couldn't find type {fullTypeName}.");
                Console.WriteLine();
                return;
            }
            var generator = new ComponentWrapperGenerator(settings);
            generator.GenerateComponentWrapper(typeToGenerate, outputFolder);
            Console.WriteLine();
        }
    }
}
