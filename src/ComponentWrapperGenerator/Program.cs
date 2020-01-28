﻿using System;
using System.IO;

namespace ComponentWrapperGenerator
{
    internal class Program
    {
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CA1801 // Parameter is never used
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        internal static int Main(string[] args)
#pragma warning restore CA1801 // Parameter is never used
#pragma warning restore IDE0060 // Remove unused parameter
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: dotnet run FileWithOneTypePerLine.txt");
                Console.WriteLine("    FileWithOneTypePerLine.txt must list types in the Xamarin.Forms namespace in the Xamarin.Forms assembly.");
                return -1;
            }

            var listOfTypeToGenerate = File.ReadAllLines(args[0]);

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

                GenerateWrapperForType(typeToGenerate, settings);
            }

            return 0;
        }

        private static void GenerateWrapperForType(string typeName, GeneratorSettings settings)
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
            generator.GenerateComponentWrapper(typeToGenerate);
            Console.WriteLine();
        }
    }
}
