// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.App;
using Android.Content;
using System;
using System.Collections.Concurrent;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    internal static class ActivityResultCallbackRegistryHelper
    {
        private static readonly ConcurrentDictionary<int, Action<Result, Intent>> s_activityResultCallbacks =
            new ConcurrentDictionary<int, Action<Result, Intent>>();
        private static int s_nextActivityResultCallbackKey;

        public static void InvokeCallback(int requestCode, Result resultCode, Intent data)
        {

            if (s_activityResultCallbacks.TryGetValue(requestCode, out var callback))
            {
                callback(resultCode, data);
            }
        }

        internal static int RegisterActivityResultCallback(Action<Result, Intent> callback)
        {
            var requestCode = s_nextActivityResultCallbackKey;

            while (!s_activityResultCallbacks.TryAdd(requestCode, callback))
            {
                s_nextActivityResultCallbackKey += 1;
                requestCode = s_nextActivityResultCallbackKey;
            }

            s_nextActivityResultCallbackKey += 1;

            return requestCode;
        }

        internal static void UnregisterActivityResultCallback(int requestCode)
        {
            s_activityResultCallbacks.TryRemove(requestCode, out var callback);
        }
    }
}
