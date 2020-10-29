// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace MobileBlazorBindingsTodo.Droid
{
    [Activity(Label = "MobileBlazorBindingsTodo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Instance = this;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Register backend-specific services
            var additionalServices = new ServiceCollection();
            additionalServices.AddSingleton<ITextToSpeech, TextToSpeech_Android>();

            LoadApplication(new App(additionalServices: additionalServices));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
