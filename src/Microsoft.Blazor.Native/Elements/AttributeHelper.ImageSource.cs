using System;
using System.Globalization;
using Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public static partial class AttributeHelper
    {
        private const string ImageFileSourcePrefix = "file:";

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
                FileImageSource fileImageSource => string.Format(CultureInfo.InvariantCulture, "file:{0}", fileImageSource.File),
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

            if (imageSourceString.StartsWith(ImageFileSourcePrefix, StringComparison.OrdinalIgnoreCase))
            {
                var imageFile = imageSourceString.Substring(ImageFileSourcePrefix.Length);
                return new FileImageSource { File = imageFile };
            }
            else
            {
                throw new NotSupportedException($"Unsupported ImageSource value: {imageSourceString}");
            }
        }
    }
}
