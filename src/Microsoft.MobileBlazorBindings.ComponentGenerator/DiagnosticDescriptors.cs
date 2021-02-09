using Microsoft.CodeAnalysis;

namespace Microsoft.MobileBlazorBindings.ComponentGenerator
{
    internal static class DiagnosticDescriptors
    {
        public static readonly DiagnosticDescriptor TypeNotFound = new DiagnosticDescriptor(
#pragma warning disable RS2008 // Enable analyzer release tracking
            id: "MBB0001",
#pragma warning restore RS2008 // Enable analyzer release tracking
            title: "Type not found",
            messageFormat: "Type {0} is not found in the project or referenced assemblies.",
            category: "ComponentGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
    }
}
