// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize objects.
        /// </summary>
        public static AttributeValueHolder ObjectToDelegate(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AttributeValueHolderFactory.FromObject(value);
        }

        /// <summary>
        /// Helper method to deserialize objects.
        /// </summary>
        public static T DelegateToObject<T>(object value, T defaultValueIfNull = default)
        {
            return AttributeValueHolderFactory.ToValue<T>(value, defaultValueIfNull);
        }
    }
}
