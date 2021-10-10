using Microsoft.MobileBlazorBindings.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.MobileBlazorBindings.UnitTests.ShellNavigation
{
    public class ShellNavigationManagerTests
    {
        private static Guid testGuid = Guid.NewGuid();
        public static IEnumerable<TestCaseData> TryParseTestData
        {
            get
            {
                yield return new TestCaseData("s", typeof(string), "s", true).SetName("Parse valid string");
                yield return new TestCaseData("5", typeof(int), 5, true).SetName("Parse valid int");
                yield return new TestCaseData("5", typeof(int?), 5, true).SetName("Parse valid int?");
                yield return new TestCaseData("invalid text", typeof(int), 0, false).SetName("Parse invalid int");
                yield return new TestCaseData("2020-05-20", typeof(DateTime), new DateTime(2020, 05, 20), true).SetName("Parse valid date");
                yield return new TestCaseData("invalid text", typeof(DateTime), new DateTime(), false).SetName("Parse invalid date");
                yield return new TestCaseData(testGuid.ToString(), typeof(Guid), testGuid, true).SetName("Parse valid GUID");
                yield return new TestCaseData(testGuid.ToString(), typeof(Guid?), testGuid, true).SetName("Parse valid GUID?");
                yield return new TestCaseData("invalid text", typeof(Guid), new Guid(), false).SetName("Parse invalid GUID");
                yield return new TestCaseData("{'value': '5'}", typeof(object), null, false).SetName("Parse POCO should find null operation");
            }
        }

        [TestCaseSource(nameof(TryParseTestData))]
        public void TryParseTest(string s, Type type, object expectedResult, bool expectedSuccess)
        {
            var success = ShellNavigationManager.TryParse(type, s, out var result);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedResult, result);
                Assert.AreEqual(expectedSuccess, success);
            });
        }
    }
}
