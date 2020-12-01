// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class MasterDetailPage : Page
    {
        [Parameter] public string MasterTitle { get; set; }

        [Parameter] public RenderFragment Master { get; set; }
        [Parameter] public RenderFragment Detail { get; set; }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            builder.OpenComponent<MasterDetailMasterPage>(0);
            builder.AddAttribute(0, nameof(MasterDetailMasterPage.ChildContent), Master);
            // TODO: This feels a bit hacky. This is really a property of the child control, but here we are defining
            // it on the container control and applying it to the child. What about other such properties?
            builder.AddAttribute(1, "Title", MasterTitle);
            builder.CloseComponent();

            builder.OpenComponent<MasterDetailDetailPage>(1);
            builder.AddAttribute(0, nameof(MasterDetailDetailPage.ChildContent), Detail);
            builder.CloseComponent();
        }
    }
}
