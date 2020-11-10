// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class MasterDetailPageHandler : PageHandler, IXamarinFormsContainerElementHandler
    {
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CA1801 // Parameter is never used
        partial void Initialize(NativeComponentRenderer renderer)
#pragma warning restore CA1801 // Parameter is never used
#pragma warning restore IDE0060 // Remove unused parameter
        {
            // Set dummy Master and Detail because this element cannot be parented unless both are set.
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L199
            // In Blazor, parents are created before children, whereas this doesn't appear to be the case in
            // Xamarin.Forms. Once the Blazor children get created, they will overwrite these dummy elements.

            // The Master page must have its Title set:
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
            MasterDetailPageControl.Master = new XF.Page() { Title = "Title" };
            MasterDetailPageControl.Detail = new XF.Page();
        }

        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child is MasterDetailMasterPageContentPage masterPage)
            {
                MasterDetailPageControl.Master = masterPage;
            }
            else if (child is MasterDetailDetailPageContentPage detailPage)
            {
                MasterDetailPageControl.Detail = detailPage;
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

            if (child == MasterDetailPageControl.Master)
            {
                MasterDetailPageControl.Master = new XF.Page() { Title = "Title" };
            }
            else if (child == MasterDetailPageControl.Detail)
            {
                MasterDetailPageControl.Detail = new XF.Page();
            }
            else
            {
                throw new InvalidOperationException($"Unknown child type {child.GetType().FullName} being removed from parent element type {GetType().FullName}.");
            }
        }
    }
}
