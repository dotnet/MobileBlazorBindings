// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellGroupItem : BaseShellItem
    {
        static ShellGroupItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellGroupItem>(
                renderer => new ShellGroupItemHandler(renderer, new MC.ShellGroupItem()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.FlyoutDisplayOptions? FlyoutDisplayOptions { get; set; }

        public new MC.ShellGroupItem NativeControl => ((ShellGroupItemHandler)ElementHandler).ShellGroupItemControl;

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

        static partial void RegisterAdditionalHandlers();
    }
}
