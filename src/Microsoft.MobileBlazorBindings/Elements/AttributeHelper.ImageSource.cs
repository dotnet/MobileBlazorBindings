// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="ImageSource" /> objects.
        /// </summary>
        public static AttributeValueHolder ImageSourceToDelegate(ImageSource imageSource)
        {
            if (imageSource is null)
            {
                throw new ArgumentNullException(nameof(imageSource));
            }

            return AttributeValueHolderFactory.FromObject(imageSource);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="ImageSource" /> objects.
        /// </summary>
        public static ImageSource DelegateToImageSource(object imageSource, ImageSource defaultValueIfNull = default)
        {
            return AttributeValueHolderFactory.ToValue<ImageSource>(imageSource, defaultValueIfNull);
        }
    }
}
