using Xamarin.Forms;

namespace MobileBlazorBindingsWeather
{
    public static class FontSizeUtility
    {
        public static double GetFontSize(NamedSize fontSize)
        {
            var converter = new FontSizeConverter();
            return (double)converter.ConvertFromInvariantString(fontSize.ToString());
        }
    }
}
