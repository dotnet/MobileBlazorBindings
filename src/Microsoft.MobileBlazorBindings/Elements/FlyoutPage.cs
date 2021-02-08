// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class FlyoutPage : Page
    {
        [Parameter] public string FlyoutTitle { get; set; }

        [Parameter] public RenderFragment Flyout { get; set; }
        [Parameter] public RenderFragment Detail { get; set; }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            builder.OpenComponent<FlyoutFlyoutPage>(0);
            builder.AddAttribute(0, nameof(FlyoutFlyoutPage.ChildContent), Flyout);
            // TODO: This feels a bit hacky. This is really a property of the child control, but here we are defining
            // it on the container control and applying it to the child. What about other such properties?
            builder.AddAttribute(1, "Title", FlyoutTitle);
            builder.CloseComponent();

            builder.OpenComponent<FlyoutDetailPage>(1);
            builder.AddAttribute(0, nameof(FlyoutDetailPage.ChildContent), Detail);
            builder.CloseComponent();
        }
    }
}
