// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.WebView.Windows;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Controls;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(XF.NavigationPage), typeof(NonAnimatedNavigationPageRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.Windows
{
    // This is a workaround for WebView (like all other HwndHost-based controls) being incompatible
    // with animated transitions. If we wanted the WebView to participate fully in WPF rendering to
    // make this work correctly, the options include:
    //
    // 1. Full off-screen rendering (like CefSharp.Wpf). However this leads to bad perf all the time.
    // 2. Snapshotting. That is, when an animation is starting, capture a bitmap of the current
    //    WebView contents, and change its renderer to display that static bitmap instead of the
    //    actual browser. Switch back as soon as the animation ends. This is very similar to how
    //    iOS handles the page previews in tab selection mode.

    public class NonAnimatedNavigationPageRenderer : NavigationPageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<XF.NavigationPage> e)
        {
            if (e is null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            if (e.OldElement != null) // Clear old element event
            {
                e.OldElement.PushRequested -= Element_PushRequested;
                e.OldElement.PopRequested -= Element_PopRequested;
                e.OldElement.PopToRootRequested -= Element_PopToRootRequested;
                e.OldElement.RemovePageRequested -= Element_RemovePageRequested;
                e.OldElement.InsertPageBeforeRequested -= Element_InsertPageBeforeRequested;
            }

            if (e.NewElement != null)
            {
                if (Control == null) // construct and SetNativeControl and suscribe control event
                {
                    SetNativeControl(new FormsLightNavigationPage(Element));
                }

                // Update control property 
                UpdateBarBackgroundColor();
                UpdateBarTextColor();

                // Suscribe element event
                Element.PushRequested += Element_PushRequested;
                Element.PopRequested += Element_PopRequested;
                Element.PopToRootRequested += Element_PopToRootRequested;
                Element.RemovePageRequested += Element_RemovePageRequested;
                Element.InsertPageBeforeRequested += Element_InsertPageBeforeRequested;
            }
        }

        private void Element_InsertPageBeforeRequested(object sender, XF.Internals.NavigationRequestedEventArgs e)
        {
            Control?.InsertPageBefore(e.Page, e.BeforePage);
        }

        private void Element_RemovePageRequested(object sender, XF.Internals.NavigationRequestedEventArgs e)
        {
            Control?.RemovePage(e.Page);
        }

        private void Element_PopToRootRequested(object sender, XF.Internals.NavigationRequestedEventArgs e)
        {
            e.Animated = false;
            Control?.PopToRoot(e.Animated);
        }

        private void Element_PopRequested(object sender, XF.Internals.NavigationRequestedEventArgs e)
        {
            e.Animated = false;
            Control?.Pop(e.Animated);
        }

        private void Element_PushRequested(object sender, XF.Internals.NavigationRequestedEventArgs e)
        {
            e.Animated = false;
            Control?.Push(e.Page, e.Animated);
        }

        private void UpdateBarBackgroundColor()
        {
            Control.UpdateDependencyColor(FormsNavigationPage.TitleBarBackgroundColorProperty, Element.BarBackgroundColor);
        }

        private void UpdateBarTextColor()
        {
            Control.UpdateDependencyColor(FormsNavigationPage.TitleBarTextColorProperty, Element.BarTextColor);
        }

        private bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (Element != null)
                {
                    Element.PushRequested -= Element_PushRequested;
                    Element.PopRequested -= Element_PopRequested;
                    Element.PopToRootRequested -= Element_PopToRootRequested;
                    Element.RemovePageRequested -= Element_RemovePageRequested;
                    Element.InsertPageBeforeRequested -= Element_InsertPageBeforeRequested;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
