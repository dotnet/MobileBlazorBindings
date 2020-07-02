// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.App;
using Android.Content;
using Android.Webkit;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;
using Object = Java.Lang.Object;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    public class WebKitWebChromeClient : WebChromeClient
    {
        private Activity _activity;
        private List<int> _requestCodes;

        public override bool OnShowFileChooser(global::Android.Webkit.WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            if (fileChooserParams is null)
            {
                throw new ArgumentNullException(nameof(fileChooserParams));
            }

            base.OnShowFileChooser(webView, filePathCallback, fileChooserParams);
            return ChooseFile(filePathCallback, fileChooserParams.CreateIntent(), fileChooserParams.Title);
        }

        public void UnregisterCallbacks()
        {
            if (_requestCodes == null || _requestCodes.Count == 0 || _activity == null)
            {
                return;
            }

            foreach (var requestCode in _requestCodes)
            {
                ActivityResultCallbackRegistryHelper.UnregisterActivityResultCallback(requestCode);
            }

            _requestCodes = null;
        }

        protected bool ChooseFile(IValueCallback filePathCallback, Intent intent, string title)
        {
            if (_activity == null)
            {
                return false;
            }

            void callback(Result resultCode, Intent intentData)
            {
                if (filePathCallback == null)
                {
                    return;
                }

                var result = ParseResult(resultCode, intentData);
                filePathCallback.OnReceiveValue(result);
            }

            _requestCodes ??= new List<int>();

            var newRequestCode = ActivityResultCallbackRegistryHelper.RegisterActivityResultCallback(callback);

            _requestCodes.Add(newRequestCode);

#pragma warning disable CA2000 // Dispose objects before losing scope
            _activity.StartActivityForResult(Intent.CreateChooser(intent, title), newRequestCode);
#pragma warning restore CA2000 // Dispose objects before losing scope

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterCallbacks();
            }

            base.Dispose(disposing);
        }

        protected virtual Object ParseResult(Result resultCode, Intent data)
        {
            return FileChooserParams.ParseResult((int)resultCode, data);
        }

        internal void SetContext(Context thisActivity)
        {
            _activity = thisActivity as Activity;
            if (_activity == null)
            {
                Log.Warning(nameof(WebKitWebViewRenderer), $"Failed to set the activity of the WebChromeClient, can't show pickers on the Webview");
            }
        }
    }
}
