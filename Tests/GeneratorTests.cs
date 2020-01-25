using NUnit.Framework;
using ComponentWrapperGenerator;
using FluentAssertions;

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
        public void GenerateLabel_Should_ContainCopyright()
        {
            var result =  generator.GenerateComponentWrapper(typeof(Xamarin.Forms.Label));

            result.Component.Content.Should().Contain("Copyright");
            result.ComponentHandler.Content.Should().Contain("Copyright");
        }

        [Test]
        public void Generate_Should_ContainNamespace()
        {
            var result = generator.GenerateComponentWrapper(typeof(Xamarin.Forms.Label));

            var namespacet = "namespace Microsoft.MobileBlazorBindings.Elements";

            result.Component.Content.Should().Contain(namespacet);
            result.ComponentHandler.Content.Should().Contain(namespacet);
        }
    }
}