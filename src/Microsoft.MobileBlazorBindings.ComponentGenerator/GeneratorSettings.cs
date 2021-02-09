// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.ComponentGenerator
{
    public class GeneratorSettings
    {
        public string FileHeader { get; set; }
        public string RootNamespace { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public string[] TypesToGenerate { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
