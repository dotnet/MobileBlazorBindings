// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBindings.UnitTests
{
    [TestFixture]
    public class NativeComponentRendererTests
    {

#pragma warning disable CA1034 // Nested types should not be visible; this is test-only code
        public class TestComponent : ComponentBase
#pragma warning restore CA1034 // Nested types should not be visible
        {
            [Parameter] public string StringParameter { get; set; }
            [Parameter] public int IntParameter { get; set; }
            [Parameter] public int? NullableIntParameter { get; set; }
            [Parameter] public object ObjectParameter { get; set; }
            public string NonParameter { get; set; }
        }

        public static IEnumerable<TestCaseData> SetParameterTestData
        {
            get
            {
                yield return new TestCaseData(new Dictionary<string, object> { { "StringParameter", "paravalue" } }).SetName("Set string parameter");
                yield return new TestCaseData(new Dictionary<string, object> { { "StringParameter", null } }).SetName("Set string parameter to null");
                yield return new TestCaseData(new Dictionary<string, object> { { "IntParameter", 5 } }).SetName("Set int parameter");
                yield return new TestCaseData(new Dictionary<string, object> { { "NullableIntParameter", 5 } }).SetName("Set int? parameter");
                yield return new TestCaseData(new Dictionary<string, object> { { "NullableIntParameter", null } }).SetName("Set int? parameter to null");
                yield return new TestCaseData(new Dictionary<string, object> { { "ObjectParameter", "stringObject" } }).SetName("Set object parameter");
            }
        }

        [TestCaseSource(nameof(SetParameterTestData))]
        public void SetParameterTest(Dictionary<string, object> parameters)
        {
            var component = new TestComponent();
            NativeComponentRenderer.SetParameterArguments(component, parameters);

            var parameterKeyValue = parameters.FirstOrDefault();
            var prop = component.GetType().GetProperty(parameterKeyValue.Key);
            var actualValue = prop.GetValue(component);
            Assert.AreEqual(parameterKeyValue.Value, actualValue);
        }

        [Test]
        public void SetIntToString()
        {
            var component = new TestComponent();
            var value = "NotAnInt";

            var parameters = new Dictionary<string, object> { { "IntParameter", value } };
            Assert.Throws<ArgumentException>(() => NativeComponentRenderer.SetParameterArguments(component, parameters));
        }

        [Test]
        public void SetNonParameter()
        {
            var component = new TestComponent();
            var value = "NonParameter";

            var parameters = new Dictionary<string, object> { { "NonParameter", value } };
            Assert.Throws<InvalidOperationException>(() => NativeComponentRenderer.SetParameterArguments(component, parameters));
        }
    }
}
