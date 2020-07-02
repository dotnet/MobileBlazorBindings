// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.IO;
using System.Xml;

namespace ComponentWrapperGenerator
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        internal static int Main(string[] args)
        {
            // Un-comment these lines for easier debugging
            //if (args.Length == 0)
            //{
            //    args = new string[] { "TypesToGenerate.txt", ".", @"..\out" };
            //}

            if (args.Length != 3)
            {
                Console.WriteLine("Usage: dotnet run FileWithOneTypePerLine.txt XmlDocsFolder ..\\OutputFolder");
                Console.WriteLine("    FileWithOneTypePerLine.txt must list types in the Xamarin.Forms namespace in the Xamarin.Forms assembly.");
                return -1;
            }

            var listOfTypeNamesToGenerate = File.ReadAllLines(args[0]);
            var outputFolder = args[2];

            var settings = new GeneratorSettings
            {
                FileHeader = @"// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
",
                RootNamespace = "Microsoft.MobileBlazorBindings.Elements",
            };

            var generator = new ComponentWrapperGenerator(settings);
            var xmlDocs = new XmlDocument();
            var xmlDocPath = Path.Combine(Directory.GetCurrentDirectory(), args[1], "Xamarin.Forms.Core.xml");
            xmlDocs.Load(xmlDocPath);

            foreach (var typeNameToGenerate in listOfTypeNamesToGenerate)
            {
                if (string.IsNullOrWhiteSpace(typeNameToGenerate))
                {
                    continue;
                }
                if (IsCommentLine(typeNameToGenerate))
                {
                    Console.WriteLine($"Skipping comment: {typeNameToGenerate}");
                    Console.WriteLine();
                    continue;
                }

                if (!TryGetTypeToGenerate(typeNameToGenerate, out var typeToGenerate))
                {
                    Console.WriteLine($"WARNING: Couldn't find type {typeNameToGenerate}.");
                    Console.WriteLine();
                    continue;
                }
                generator.GenerateComponentWrapper(typeToGenerate, xmlDocs, outputFolder);
                Console.WriteLine();
            }

            return 0;
        }

        private static bool IsCommentLine(string typeNameToGenerate)
        {
            return typeNameToGenerate[0] == '#';
        }

        private static bool TryGetTypeToGenerate(string typeName, out Type typeToGenerate)
        {
            var fullTypeName = "Xamarin.Forms." + typeName;
            typeToGenerate = typeof(Xamarin.Forms.Element).Assembly.GetType(fullTypeName);
            return typeToGenerate != null;
        }
    }
}
