// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed partial class MasterPageHandler : ContentPageHandler
    {
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CA1801 // Parameter is never used
        partial void Initialize(NativeComponentRenderer renderer)
#pragma warning restore CA1801 // Parameter is never used
#pragma warning restore IDE0060 // Remove unused parameter
        {
            // The Master page must have its Title set:
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
            ContentPageControl.Title = "Title";
        }
    }
}
