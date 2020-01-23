namespace ComponentWrapperGenerator
{
    // TODO: XML Doc Comments

    internal class Program
    {
#pragma warning disable IDE0060 // Remove unused parameter
        internal static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var settings = new GeneratorSettings
            {
                FileHeader = @"// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
",
                RootNamespace = "Microsoft.MobileBlazorBindings.Elements",
            };

            var typeToGenerate = typeof(Xamarin.Forms.Entry);
            var generator = new ComponentWrapperGenerator(settings);
            generator.GenerateComponentWrapper(typeToGenerate);
        }
    }
}
