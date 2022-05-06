// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.App;
using Android.Content.PM;

namespace BlazorBindingsXaminals
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}