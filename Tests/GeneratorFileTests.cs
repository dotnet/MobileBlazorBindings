using System;
using System.IO;
using ComponentWrapperGenerator;
using NUnit.Framework;

namespace Tests
{
    public class GeneratorFileTests
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

            settings.ResultPath = @"..\..\..\..\Microsoft.MobileBlazorBindings.Components";
            generator = new Generator(settings);
        }

        [Test]
        public void Generate_Label_Should_WriteToFile()
        {
            generator.GenerateComponentWrapperToFile(typeof(Xamarin.Forms.Label));
        }

    }
}