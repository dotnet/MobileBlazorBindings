using Xamarin.Forms;

namespace MobileBlazorBindingsWeather
{
    public static class AppResources
    {
        public static readonly Color WarmStartColor = Color.FromHex("#F6CC66");
        public static readonly Color WarmEndColor = Color.FromHex("#FCA184");
        public static readonly Color NightStartColor = Color.FromHex("#172941");
        public static readonly Color NightEndColor = Color.FromHex("#3C6683");
        public static readonly Color ColdStartColor = Color.FromHex("#BDE3FA");
        public static readonly Color ColdEndColor = Color.FromHex("#A5C9FD");

        public static readonly Color MainTextColor = Color.White;

        public static class LabelTemperatureStyle
        {
            public static readonly double FontSize = 100;
            public static readonly LayoutOptions HorizontalOptions = LayoutOptions.Center;
            public static readonly Color TextColor = MainTextColor;
        }
    }
}
