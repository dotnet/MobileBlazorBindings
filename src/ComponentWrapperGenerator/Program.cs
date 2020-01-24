using System;

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
                Console.WriteLine("Usage: dotnet run TypeName");
                Console.WriteLine("    TypeName must be in the Xamarin.Forms namespace in the Xamarin.Forms assembly.");
                return -1;
            }

            var settings = new GeneratorSettings
            {
                FileHeader = @"// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
",
                RootNamespace = "Microsoft.MobileBlazorBindings.Elements",
            };

            var typeToGenerate = typeof(Xamarin.Forms.Element).Assembly.GetType("Xamarin.Forms." + args[0]);
            var generator = new ComponentWrapperGenerator(settings);
            generator.GenerateComponentWrapper(typeToGenerate);

            return 0;
        }
    }
}
