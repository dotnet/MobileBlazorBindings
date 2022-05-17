// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.Elements;
using NUnit.Framework;

namespace BlazorBindings.UnitTests.Elements
{
    public class AttributeHelperTests
    {
        [Test]
        public void AttributeValueShouldReturnSameObjectInstance()
        {
            var originalInstance = new object();
            var attributeValue = AttributeHelper.ObjectToDelegate(originalInstance);
            var retrievedInstance = AttributeHelper.DelegateToObject<object>(attributeValue);

            Assert.That(retrievedInstance, Is.SameAs(originalInstance));
        }

        [Test]
        public void AttributeValuesShouldBeEqualForTheSameObject()
        {
            var instance = new object();

            var attributeValue1 = AttributeHelper.ObjectToDelegate(instance);
            var attributeValue2 = AttributeHelper.ObjectToDelegate(instance);

            Assert.That(attributeValue1.Equals(attributeValue2));
        }

        [Test]
        public void AttributeValuesShouldNotBeEqualForDifferentObject()
        {
            var instance1 = new object();
            var instance2 = new object();

            var attributeValue1 = AttributeHelper.ObjectToDelegate(instance1);
            var attributeValue2 = AttributeHelper.ObjectToDelegate(instance2);

            Assert.That(!attributeValue1.Equals(attributeValue2));
        }
    }
}
