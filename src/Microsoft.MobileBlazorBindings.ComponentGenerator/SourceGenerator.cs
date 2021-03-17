using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Linq;

namespace Microsoft.MobileBlazorBindings.ComponentGenerator
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        private const string SettingsFileName = "component-generator.json";

        public void Execute(GeneratorExecutionContext context)
        {
            var settingsFile = context.AdditionalFiles
                .FirstOrDefault(f => string.Equals(Path.GetFileName(f.Path), SettingsFileName, StringComparison.OrdinalIgnoreCase));

            if (settingsFile is null)
            {
                return;
            }

            var settings = GeneratorSettingsParser.Parse(settingsFile.GetText().ToString());

            if (settings.TypesToGenerate is null)
            {
                return;
            }
            var componentGenerator = new ComponentWrapperGenerator(settings);

            foreach (var typeName in settings.TypesToGenerate)
            {
                var type = context.Compilation.GetTypeByMetadataName(typeName);

                if (type == null)
                {
                    var diagnostic = Diagnostic.Create(DiagnosticDescriptors.TypeNotFound,
                        Location.Create(settingsFile.Path, default, default),
                        typeName);
                    context.ReportDiagnostic(diagnostic);
                }
                else
                {
                    var generatedFiles = componentGenerator.GenerateComponentWrapper(type);

                    foreach (var (hintName, source) in generatedFiles)
                    {
                        context.AddSource(hintName, source);
                    }
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
