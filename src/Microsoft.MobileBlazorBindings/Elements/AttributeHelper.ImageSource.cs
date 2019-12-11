// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        private const string FileImageSourcePrefix = "file";
        private const string UriImageSourcePrefix = "uri";

        /// <summary>
        /// Helper method to serialize <see cref="ImageSource" /> objects.
        /// </summary>
        public static string ImageSourceToString(ImageSource imageSource)
        {
            if (imageSource is null)
            {
                throw new ArgumentNullException(nameof(imageSource));
            }

            return imageSource switch
            {
                FileImageSource fileImageSource => string.Format(CultureInfo.InvariantCulture, "{0}:{1}", FileImageSourcePrefix, fileImageSource.File),
                UriImageSource uriImageSource => string.Format(CultureInfo.InvariantCulture, "{0}:{1}", UriImageSourcePrefix, uriImageSource.Uri.ToString()),
                _ => throw new NotSupportedException($"Unsupported ImageSource type: {imageSource.GetType().FullName}."),
            };
        }

        /// <summary>
        /// Helper method to deserialize <see cref="ImageSource" /> objects.
        /// </summary>
        public static ImageSource StringToImageSource(string imageSourceString)
        {
            if (imageSourceString is null)
            {
                throw new ArgumentNullException(nameof(imageSourceString));
            }

            var indexOfColon = imageSourceString.IndexOf(':');
            if (indexOfColon == -1)
            {
                throw new ArgumentException($"Invalid image source format: '{imageSourceString}'", nameof(imageSourceString));
            }

            var prefix = imageSourceString.Substring(0, indexOfColon);
            var data = imageSourceString.Substring(indexOfColon + 1);

            return prefix switch
            {
                FileImageSourcePrefix => new FileImageSource { File = data },
                UriImageSourcePrefix => new UriImageSource { Uri = new Uri(data) },
                _ => throw new NotSupportedException($"Unsupported ImageSource value: {imageSourceString}"),
            };
        }
    }
}
