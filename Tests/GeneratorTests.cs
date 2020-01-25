using NUnit.Framework;
using ComponentWrapperGenerator;

namespace Tests
{
    public class GeneratorTests
    {
        private Generator generator;
        [SetUp]
        public void Setup()
        {
            var settings = new GeneratorSettings();
            settings.FileHeader = @"// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
";
            settings.RootNamespace = "Microsoft.MobileBlazorBindings.Elements";
            generator = new Generator(settings);
        }

        [Test]
        public void GenerateLabel()
        {
            
        }
    }
}