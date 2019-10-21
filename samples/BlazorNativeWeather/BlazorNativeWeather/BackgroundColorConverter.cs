using Xamarin.Forms;

namespace BlazorNativeWeather
{
    public static class BackgroundColorConverter
    {
        public static Color GetColor(int temp, bool isStart)
        {
            if (temp > 60)
            {
                return isStart ? AppResources.WarmStartColor : AppResources.WarmEndColor;
            }
            else if (temp == -100)
            {
                return isStart ? AppResources.NightStartColor : AppResources.NightEndColor;
            }
            else
            {
                return isStart ? AppResources.ColdStartColor : AppResources.ColdEndColor;
            }
        }
    }
}
