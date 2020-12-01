// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Shell : Page
    {
        [Parameter] public RenderFragment FlyoutHeader { get; set; }

        [Parameter] public EventCallback<XF.ShellNavigatedEventArgs> OnNavigated { get; set; }
        [Parameter] public EventCallback<XF.ShellNavigatingEventArgs> OnNavigating { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onnavigated", OnNavigated);
            builder.AddAttribute("onnavigating", OnNavigating);
        }

        public async Task GoTo(XF.ShellNavigationState state, bool animate = true)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            await NativeControl.GoToAsync(state, animate).ConfigureAwait(true);
        }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            if (FlyoutHeader != null)
            {
                builder.OpenComponent<ShellFlyoutHeader>(1);
                builder.AddAttribute(0, nameof(ChildContent), FlyoutHeader);
                builder.CloseComponent();
            }

            builder.AddContent(2, ChildContent);
        }
    }
}
