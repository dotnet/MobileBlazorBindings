// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ComponentWrapperGenerator
{
    internal class Program
    {
        private static readonly List<ComponentLocation> ComponentLocations =
            new List<ComponentLocation>
            {
                new ComponentLocation(typeof(Xamarin.Forms.Element).Assembly, "Xamarin.Forms", "XF", @"bin\Debug\netcoreapp3.0\Xamarin.Forms.Core.xml"),
                new ComponentLocation(typeof(Xamarin.Forms.DualScreen.TwoPaneView).Assembly, "Xamarin.Forms.DualScreen", "XFD", null),
            };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        internal static int Main(string[] args)
        {
            // Un-comment these lines for easier debugging
            if (args.Length == 0)
            {
                args = new string[] { "TypesToGenerate.txt", @"..\out" };
            }

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run FileWithOneTypePerLine.txt ..\\OutputFolder");
                Console.WriteLine("    FileWithOneTypePerLine.txt must list types in the Xamarin.Forms[.DualScreen] namespace in the Xamarin.Forms[.DualScreen] assembly.");
                return -1;
            }

            var listOfTypeNamesToGenerate = File.ReadAllLines(args[0]);
            var outputFolder = args[1];

            var settings = new GeneratorSettings
            {
                FileHeader = @"// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
",
                RootNamespace = "Microsoft.MobileBlazorBindings.Elements",
            };

            var xmlDocs = LoadXmlDocs(ComponentLocations.Select(loc => loc.XmlDocFilename).Where(loc => loc != null));

            var generator = new ComponentWrapperGenerator(settings, xmlDocs);

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
                generator.GenerateComponentWrapper(typeToGenerate, outputFolder);
                Console.WriteLine();
            }

            return 0;
        }

        private static IList<XmlDocument> LoadXmlDocs(IEnumerable<string> xmlDocLocations)
        {
            var xmlDocs = new List<XmlDocument>();
            foreach (var xmlDocLocation in xmlDocLocations)
            {
                var xmlDoc = new XmlDocument();

                // Depending on whether you run from VS or command line, the relative path of the XML docs will be
                // different. There's undoubtedly a better way to do this, but this works great.
                var xmlDocPath = Path.Combine(Directory.GetCurrentDirectory(), xmlDocLocation);
                if (!File.Exists(xmlDocPath))
                {
                    xmlDocPath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(xmlDocLocation));
                }

                xmlDoc.Load(xmlDocPath);
                xmlDocs.Add(xmlDoc);
            }
            return xmlDocs;
        }

        private static bool IsCommentLine(string typeNameToGenerate)
        {
            return typeNameToGenerate[0] == '#';
        }

        private static bool TryGetTypeToGenerate(string typeName, out Type typeToGenerate)
        {
            foreach (var componentLocation in ComponentLocations)
            {
                var fullTypeName = componentLocation.NamespaceName + "." + typeName;
                typeToGenerate = componentLocation.Assembly.GetType(fullTypeName);
                if (typeToGenerate != null)
                {
                    return true;
                }
            }

            typeToGenerate = null;
            return false;
        }
    }
}
