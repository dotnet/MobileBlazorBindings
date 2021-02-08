// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FlyoutPageHandler : PageHandler, IXamarinFormsContainerElementHandler
    {
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CA1801 // Parameter is never used
        partial void Initialize(NativeComponentRenderer renderer)
#pragma warning restore CA1801 // Parameter is never used
#pragma warning restore IDE0060 // Remove unused parameter
        {
            // Set dummy Flyout and Detail because this element cannot be parented unless both are set.
            // https://github.com/xamarin/Xamarin.Forms/blob/5.0.0/Xamarin.Forms.Core/FlyoutPage.cs#L199
            // In Blazor, parents are created before children, whereas this doesn't appear to be the case in
            // Xamarin.Forms. Once the Blazor children get created, they will overwrite these dummy elements.

            // The Flyout page must have its Title set:
            // https://github.com/xamarin/Xamarin.Forms/blob/5.0.0/Xamarin.Forms.Core/FlyoutPage.cs#L72
            FlyoutPageControl.Flyout = new XF.Page() { Title = "Title" };
            FlyoutPageControl.Detail = new XF.Page();
        }

        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child is FlyoutFlyoutPageContentPage masterPage)
            {
                FlyoutPageControl.Flyout = masterPage;
            }
            else if (child is FlyoutDetailPageContentPage detailPage)
            {
                FlyoutPageControl.Detail = detailPage;
            }
            else
            {
                throw new InvalidOperationException($"Unknown child type {child.GetType().FullName} being added to parent element type {GetType().FullName}.");
            }
        }

        public virtual void RemoveChild(XF.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child == FlyoutPageControl.Flyout)
            {
                FlyoutPageControl.Flyout = new XF.Page() { Title = "Title" };
            }
            else if (child == FlyoutPageControl.Detail)
            {
                FlyoutPageControl.Detail = new XF.Page();
            }
            else
            {
                throw new InvalidOperationException($"Unknown child type {child.GetType().FullName} being removed from parent element type {GetType().FullName}.");
            }
        }

        public int GetChildIndex(XF.Element child)
        {
            // Not sure whether elements "order" matters here
            return child switch
            {
                _ when child == FlyoutPageControl.Flyout => 0,
                _ when child == FlyoutPageControl.Detail => 1,
                _ => -1
            };
        }
    }
}
