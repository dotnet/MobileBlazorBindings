using Microsoft.AspNetCore.Components;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.UnitTests
{
    public class SetNavigationParameterTests
    {
        public class TestComponent : ComponentBase
        {
            [Parameter] public string StringParameter { get; set; }
            [Parameter] public int IntParameter { get; set; }
            public string NonParameter { get; set; }
        }
        [Test]
        public void SetString()
        {
            var component = new TestComponent();
            var expected = "TestValue";

            var parameters = new Dictionary<string, string> {  { "StringParameter", expected } };
            component.SetNavigationParameters(parameters);

            Assert.AreEqual(expected, component.StringParameter);
        }

        [Test]
        public void SetInt()
        {
            var component = new TestComponent();
            var expected = 5;

            var parameters = new Dictionary<string, string> { { "IntParameter", expected.ToString() } };
            component.SetNavigationParameters(parameters);

            Assert.AreEqual(expected, component.IntParameter);
        }

        [Test]
        public void SetIntToString()
        {
            var component = new TestComponent();
            var expected = "NotAnInt";

            var parameters = new Dictionary<string, string> { { "IntParameter", expected } };
            Assert.Throws<InvalidCastException>(() => component.SetNavigationParameters(parameters));
        }

        [Test]
        public void SetNonParameter()
        {
            var component = new TestComponent();
            var expected = "NonParameter";

            var parameters = new Dictionary<string, string> { { "NonParameter", expected } };
            Assert.Throws<InvalidOperationException>(() => component.SetNavigationParameters(parameters));
        }
    }
}
