// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.App;
using Android.Content;
using Android.Content.PM;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Handles callback for authentication on Android.
    /// </summary>
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "app")]
    public class WebAuthenticationCallbackActivity : Maui.Authentication.WebAuthenticatorCallbackActivity
    {
    }
}
