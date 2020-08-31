// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Microsoft.MobileBlazorBindings.Elements
{
    /// <summary>
    /// Utility class to convert between <see cref="AttributeValueHolder"/> instances and
    /// the data being held.
    /// </summary>
    public static class AttributeValueHolderFactory
    {
        private sealed class DataHolder<TAttributeValue>
        {
            private readonly TAttributeValue _attributeValue;

            internal DataHolder(TAttributeValue attributeValue)
            {
                _attributeValue = attributeValue;
            }

            internal void ProduceValue(out object attributeValue)
            {
                attributeValue = _attributeValue;
            }
        }

        /// <summary>
        /// Returns an <see cref="AttributeValueHolder"/> representing the data provided in <paramref name="attributeValue"/>.
        /// </summary>
        /// <typeparam name="TAttributeValue"></typeparam>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static AttributeValueHolder FromObject<TAttributeValue>(TAttributeValue attributeValue)
        {
            return new AttributeValueHolder(new DataHolder<TAttributeValue>(attributeValue).ProduceValue);
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

            attributeValueAsValueHolder(out var value);

            if (!(value is TAttributeValue typedAttributeValue))
            {
                throw new ArgumentException($"Expected attribute value to be an {typeof(TAttributeValue).FullName}.", nameof(attributeValue));
            }

            return typedAttributeValue;
        }
    }
}
