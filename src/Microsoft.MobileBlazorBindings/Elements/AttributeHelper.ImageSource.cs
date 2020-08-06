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
        private const string FontImageSourcePrefix = "font";
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
                FontImageSource fontImageSource => SerializeFontImageSource(fontImageSource),
                UriImageSource uriImageSource => string.Format(CultureInfo.InvariantCulture, "{0}:{1}", UriImageSourcePrefix, uriImageSource.Uri.ToString()),
                _ => throw new NotSupportedException($"Unsupported ImageSource type: {imageSource.GetType().FullName}."),
            };
        }

        /// <summary>
        /// Helper method to deserialize <see cref="ImageSource" /> objects.
        /// </summary>
        public static ImageSource StringToImageSource(object imageSourceString, ImageSource defaultValueIfNull = default)
        {
            if (imageSourceString is null)
            {
                return defaultValueIfNull;
            }
            if (!(imageSourceString is string imageSourceAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(imageSourceString));
            }

            var indexOfColon = imageSourceAsString.IndexOf(':');
            if (indexOfColon == -1)
            {
                throw new ArgumentException($"Invalid image source format: '{imageSourceString}'", nameof(imageSourceString));
            }

            var prefix = imageSourceAsString.Substring(0, indexOfColon);
            var data = imageSourceAsString.Substring(indexOfColon + 1);

            return prefix switch
            {
                FileImageSourcePrefix => new FileImageSource { File = data },
                FontImageSourcePrefix => DeserializeFontImageSource(data),
                UriImageSourcePrefix => new UriImageSource { Uri = new Uri(data) },
                _ => throw new NotSupportedException($"Unsupported ImageSource value: {imageSourceString}"),
            };
        }

        private static string SerializeFontImageSource(FontImageSource fontImageSource)
        {
            var fontImageString = string.Format(
                CultureInfo.InvariantCulture,
                "{0}:{1}:{2}:{3}:{4}",
                FontImageSourcePrefix,
                ColorToString(fontImageSource.Color),
                fontImageSource.FontFamily,
                DoubleToString(fontImageSource.Size),
                fontImageSource.Glyph);
            return fontImageString;
        }

        private static FontImageSource DeserializeFontImageSource(string data)
        {
            // Split to 4 segments max because the last segment (glyph) might have colons
            var parts = data.Split(new[] { ':' }, count: 4);

            return new FontImageSource
            {
                Color = StringToColor(parts[0]),
                FontFamily = parts[1],
                Size = StringToDouble(parts[2]),
                Glyph = parts[3],
            };
        }
    }
}
