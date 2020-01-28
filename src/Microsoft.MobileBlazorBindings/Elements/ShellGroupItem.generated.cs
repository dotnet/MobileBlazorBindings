// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ShellGroupItem : BaseShellItem
    {
        static ShellGroupItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellGroupItem>(
                renderer => new ShellGroupItemHandler(renderer, new XF.ShellGroupItem()));
        }

        [Parameter] public XF.FlyoutDisplayOptions? FlyoutDisplayOptions { get; set; }

        public new XF.ShellGroupItem NativeControl => ((ShellGroupItemHandler)ElementHandler).ShellGroupItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutDisplayOptions != null)
            {
                builder.AddAttribute(nameof(FlyoutDisplayOptions), (int)FlyoutDisplayOptions.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
