// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Extensions;
using System;

namespace Microsoft.MobileBlazorBindings.Elements
{
    /// <summary>
    /// Utility class to convert between <see cref="AttributeValueHolder"/> instances and
    /// the data being held.
    /// </summary>
    public static class AttributeValueHolderFactory
    {
        /// <summary>
        /// Returns an <see cref="AttributeValueHolder"/> representing the data provided in <paramref name="attributeValue"/>.
        /// </summary>
        /// <typeparam name="TAttributeValue"></typeparam>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static AttributeValueHolder FromObject<TAttributeValue>(TAttributeValue attributeValue)
        {
            // Creating delegate from extension method parameter preserves equality for delegate,
            // i.e. two delegates are equal if held values are equal.
            return attributeValue.This;
        }

        /// <summary>
        /// Produces the value represnted by <paramref name="attributeValue"/>. If the value is <c>null</c>, the value
        /// specified in <paramref name="defaultValueIfNull"/> is returned.
        /// </summary>
        /// <typeparam name="TAttributeValue"></typeparam>
        /// <param name="attributeValue"></param>
        /// <param name="defaultValueIfNull"></param>
        /// <returns></returns>
        public static TAttributeValue ToValue<TAttributeValue>(object attributeValue, TAttributeValue defaultValueIfNull = default)
        {
            if (attributeValue is null)
            {
                return defaultValueIfNull;
            }
            if (!(attributeValue is AttributeValueHolder attributeValueAsValueHolder))
            {
                throw new ArgumentException("Expected parameter instance to be an attribute value holder.", nameof(attributeValue));
            }

            var value = attributeValueAsValueHolder();

            if (!(value is TAttributeValue typedAttributeValue))
            {
                throw new ArgumentException($"Expected attribute value to be an {typeof(TAttributeValue).FullName}.", nameof(attributeValue));
            }

            return typedAttributeValue;
        }
    }
}
