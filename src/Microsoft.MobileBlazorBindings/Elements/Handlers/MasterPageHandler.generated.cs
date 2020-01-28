// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed partial class MasterPageHandler : ContentPageHandler
    {
        public MasterPageHandler(NativeComponentRenderer renderer, XF.ContentPage masterDetailPageControl) : base(renderer, masterDetailPageControl)
        {
            MasterDetailPageControl = masterDetailPageControl ?? throw new ArgumentNullException(nameof(masterDetailPageControl));

            // The Master page must have its Title set:
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
            ContentPageControl.Title = "Title";

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ContentPage MasterDetailPageControl { get; }
    }
}
