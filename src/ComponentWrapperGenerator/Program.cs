// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ComponentWrapperGenerator
{
    using Microsoft.MobileBlazorBindings.ComponentGenerator;

    internal class Program
    {
        internal static async Task<int> Main(string[] args)
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

            var compilation = await GetRoslynCompilationAsync().ConfigureAwait(false);
            var generator = new ComponentWrapperGenerator(settings);

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

                var typeToGenerate = compilation.GetTypeByMetadataName(typeNameToGenerate);

                if (typeToGenerate == null)
                {
                    Console.WriteLine($"WARNING: Couldn't find type {typeNameToGenerate}.");
                    Console.WriteLine();
                    continue;
                }

                var generatedSources = generator.GenerateComponentWrapper(typeToGenerate);
                WriteFiles(generatedSources, outputFolder);

                Console.WriteLine();
            }

            return 0;
        }

        private static void WriteFiles((string HintName, string Source)[] generatedSources, string outputFolder)
        {
            foreach(var (hintName, source) in generatedSources)
            {
                var fileName = $"{hintName}.generated.cs";
                var path = hintName.EndsWith("Handler", StringComparison.Ordinal)
                    ? Path.Combine(outputFolder, "Handlers")
                    : outputFolder;

                Directory.CreateDirectory(path);

                File.WriteAllText(Path.Combine(path, fileName), source);
            }
        }

        private static async Task<Compilation> GetRoslynCompilationAsync()
        {
            var metadataReferences = new[] {
                MetadataReferenceFromAssembly(typeof(Xamarin.Forms.Element).Assembly),
                MetadataReferenceFromAssembly(typeof(Xamarin.Forms.DualScreen.TwoPaneView).Assembly),
                MetadataReferenceFromAssembly(typeof(object).Assembly),
                MetadataReferenceFromAssembly(Assembly.Load(new AssemblyName("System.Runtime"))),
                MetadataReferenceFromAssembly(Assembly.Load(new AssemblyName("netstandard")))
            };

            var projectName = "NewProject";
            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), projectName, projectName, LanguageNames.CSharp,
                metadataReferences: metadataReferences);

            using var workspace = new AdhocWorkspace();
            var project = workspace.AddProject(projectInfo);

            return await project.GetCompilationAsync().ConfigureAwait(false);
        }

        private static PortableExecutableReference MetadataReferenceFromAssembly(Assembly assembly)
        {
            var assemblyPath = assembly.Location;
            var xmlDocPath = Path.ChangeExtension(assemblyPath, "xml");
            var docProvider = File.Exists(xmlDocPath) ? XmlDocumentationProvider.CreateFromFile(xmlDocPath) : null;

            return MetadataReference.CreateFromFile(assemblyPath, documentation: docProvider);
        }

        private static bool IsCommentLine(string typeNameToGenerate)
        {
            return typeNameToGenerate[0] == '#';
        }
    }
}
