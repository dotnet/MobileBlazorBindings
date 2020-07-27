using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.UnitTests
{
    public class ParamerterParsingTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPass()
        {
            Assert.Pass();
        }

        [Test]
        public void ParseValidString()
        {
            var s = "s";
            var type = typeof(string);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsTrue(convertedSuccessfully);
        }

        [Test]
        public void ParseValidInt()
        {
            var s = "5";
            var type = typeof(int);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsTrue(convertedSuccessfully);
        }

        [Test]
        public void ParseInvalidInt()
        {
            var s = "I'm not an int";
            var type = typeof(int);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsFalse(convertedSuccessfully);
        }
    }
}
