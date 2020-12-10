// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Runtime.InteropServices;
using TWebView = Tizen.WebView.WebView;

namespace Microsoft.MobileBlazorBindings.WebView.Tizen
{
    public static class WebViewExtension
    {
        public const string ChromiumEwk = "libchromium-ewk.so";

        public static void SetInterceptRequestCallback(this TWebView webView, InterceptRequestCallback callback)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            var context = webView.GetContext();
#pragma warning restore CA1062 // Validate arguments of public methods
            var handleField = context.GetType().GetField("_handle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            IntPtr contextHandle = (IntPtr)handleField.GetValue(context);
            ewk_context_intercept_request_callback_set(contextHandle, callback, IntPtr.Zero);
        }

        public static bool SetInterceptRequestResponse(this TWebView webView, IntPtr request, string header, string body, uint length)
        {
            return ewk_intercept_request_response_set(request, header, body, length);
        }

        public static bool IgnoreInterceptRequest(this TWebView webView, IntPtr request)
        {
            return ewk_intercept_request_ignore(request);
        }

#pragma warning disable CA1055 // URI-like return values should not be strings
        public static string GetInterceptRequestUrl(this TWebView webView, IntPtr request)
#pragma warning restore CA1055 // URI-like return values should not be strings
        {
            return Marshal.PtrToStringAnsi(_ewk_intercept_request_url_get(request));
        }

        [DllImport(ChromiumEwk)]
        internal static extern IntPtr ewk_view_context_get(IntPtr obj);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void InterceptRequestCallback(IntPtr context, IntPtr request, IntPtr userData);

        [DllImport(ChromiumEwk)]
        internal static extern void ewk_context_intercept_request_callback_set(IntPtr context, InterceptRequestCallback callback, IntPtr userData);
    
        [DllImport(ChromiumEwk, EntryPoint = "ewk_intercept_request_url_get")]
        internal static extern IntPtr _ewk_intercept_request_url_get(IntPtr request);

        [DllImport(ChromiumEwk, EntryPoint = "ewk_intercept_request_http_method_get")]
        internal static extern IntPtr _ewk_intercept_request_http_method_get(IntPtr request);

        internal static string ewk_intercept_request_http_method_get(IntPtr request)
        {
            return Marshal.PtrToStringAnsi(_ewk_intercept_request_http_method_get(request));
        }

        [DllImport(ChromiumEwk)]
        internal static extern bool ewk_intercept_request_ignore(IntPtr request);

#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
        [DllImport(ChromiumEwk)]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
        internal static extern bool ewk_intercept_request_response_set(IntPtr request, string header, string body, uint length);
    }
}
