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

        [Test]
        public void ParseValidDate()
        {
            var s = "2020-01-22";
            var type = typeof(DateTime);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsTrue(convertedSuccessfully);
        }

        [Test]
        public void ParseInvalidDate()
        {
            var s = "202s-01-22";
            var type = typeof(DateTime);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsFalse(convertedSuccessfully);
        }

        [Test]
        public void ParseValidGuid()
        {
            var s = Guid.NewGuid().ToString();
            var type = typeof(Guid);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsTrue(convertedSuccessfully);
        }

        [Test]
        public void ParseInvalidGuid()
        {
            var s = "202s-01-22";
            var type = typeof(Guid);
            var convertedSuccessfully = type.TryParse(s, out object result);

            Assert.IsFalse(convertedSuccessfully);
        }
    }
}
