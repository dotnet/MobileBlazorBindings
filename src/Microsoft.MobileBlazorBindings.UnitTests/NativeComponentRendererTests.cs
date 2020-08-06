// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.MobileBlazorBindings.UnitTests
{
    [TestFixture]
    public class NativeComponentRendererTests
    {
        [SetUp]
        public void Setup()
        {
        }
        private static Guid testGuid = Guid.NewGuid();
        public static IEnumerable<TestCaseData> TryParseTestData
        {
            get
            {
                yield return new TestCaseData("s", typeof(string), "s", true).SetName("Parse valid string");
                yield return new TestCaseData("5", typeof(int), 5, true).SetName("Parse valid int");
                yield return new TestCaseData("invalid text", typeof(int), 0, false).SetName("Parse invalid int");
                yield return new TestCaseData("2020-05-20", typeof(DateTime), new DateTime(2020, 05, 20), true).SetName("Parse valid date");
                yield return new TestCaseData("invalid text", typeof(DateTime), new DateTime(), false).SetName("Parse invalid date");
                yield return new TestCaseData(testGuid.ToString(), typeof(Guid), testGuid, true).SetName("Parse valid GUID");
                yield return new TestCaseData("invalid text", typeof(Guid), new Guid(), false).SetName("Parse invalid GUID");
                yield return new TestCaseData("{'value': '5'}", typeof(object), null, false).SetName("Parse POCO should find null operation");
            }
        }

        [TestCaseSource(typeof(NativeComponentRendererTests), nameof(TryParseTestData))]
        public void TryParseTest(string s, Type type, object expectedResult, bool expectedSuccess)
        {
            var success = NativeComponentRenderer.TryParse(type, s, out var result);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedResult, result);
                Assert.AreEqual(expectedSuccess, success);
            });
        }

#pragma warning disable CA1034 // Nested types should not be visible; this is test-only code
        public class TestComponent : ComponentBase
#pragma warning restore CA1034 // Nested types should not be visible
        {
            [Parameter] public string StringParameter { get; set; }
            [Parameter] public int IntParameter { get; set; }
            public string NonParameter { get; set; }
        }

        public static IEnumerable<TestCaseData> SetParameterTestData
        {
            get
            {
                yield return new TestCaseData(new Dictionary<string, string> { { "StringParameter", "paravalue" } }, "paravalue").SetName("Set string parameter");
                yield return new TestCaseData(new Dictionary<string, string> { { "IntParameter", "5" } }, 5).SetName("Set int parameter");
            }
        }

        [TestCaseSource(typeof(NativeComponentRendererTests), nameof(SetParameterTestData))]
        public void SetParameterTest(Dictionary<string, string> parameters, object expected)
        {
            var component = new TestComponent();
            NativeComponentRenderer.SetNavigationParameters(component, parameters);

            var prop = component.GetType().GetProperty(parameters.FirstOrDefault().Key);
            var value = prop.GetValue(component);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void SetIntToString()
        {
            var component = new TestComponent();
            var expected = "NotAnInt";

            var parameters = new Dictionary<string, string> { { "IntParameter", expected } };
            Assert.Throws<InvalidOperationException>(() => NativeComponentRenderer.SetNavigationParameters(component, parameters));
        }

        [Test]
        public void SetNonParameter()
        {
            var component = new TestComponent();
            var expected = "NonParameter";

            var parameters = new Dictionary<string, string> { { "NonParameter", expected } };
            Assert.Throws<InvalidOperationException>(() => NativeComponentRenderer.SetNavigationParameters(component, parameters));
        }
    }
}
