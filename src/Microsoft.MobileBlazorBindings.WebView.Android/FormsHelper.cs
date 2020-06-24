using Android.OS;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    internal static class FormsHelper
    {
        private static BuildVersionCodes? s_sdkInt;

        internal static BuildVersionCodes SdkInt
        {
            get
            {
                if (!s_sdkInt.HasValue)
                {
                    s_sdkInt = Build.VERSION.SdkInt;
                }

                return s_sdkInt.Value;
            }
        }
    }
}
