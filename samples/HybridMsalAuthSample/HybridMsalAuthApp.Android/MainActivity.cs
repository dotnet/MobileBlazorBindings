// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Microsoft.Identity.Client;
using Microsoft.MobileBlazorBindings.WebView.Android;

namespace HybridMsalAuthApp.Droid
{
    [Activity(Label = "HybridMsalAuthApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            BlazorHybridAndroid.Init();

            var fileProvider = new AssetFileProvider(this.Assets, "wwwroot");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(fileProvider: fileProvider));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] global::Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode,
                                                 Result resultCode,
                                                 Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Return control to MSAL
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode,
                                                                                    resultCode,
                                                                                    data);
        }

        protected override void OnResume()
        {
            Xamarin.Essentials.Platform.OnResume();
            base.OnResume();
        }

    }

    /// <summary>
    /// Intent filter to capture the return request. The DataScheme is in the
    /// format msal{clientId} where {clientId} needs to be replaced with the
    /// client Id.
    /// </summary>
    [Activity]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal{clientId}")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
