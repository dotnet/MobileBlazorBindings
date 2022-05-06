// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class BaseShellItem : NavigableElement
    {
        static BaseShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<BaseShellItem>(
                renderer => new BaseShellItemHandler(renderer, new MC.BaseShellItem()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.ImageSource FlyoutIcon { get; set; }
        [Parameter] public bool? FlyoutItemIsVisible { get; set; }
        [Parameter] public MC.ImageSource Icon { get; set; }
        [Parameter] public bool? IsEnabled { get; set; }
        [Parameter] public bool? IsVisible { get; set; }
        [Parameter] public string Route { get; set; }
        [Parameter] public string Title { get; set; }

        public new MC.BaseShellItem NativeControl => ((BaseShellItemHandler)ElementHandler).BaseShellItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutIcon != null)
            {
                builder.AddAttribute(nameof(FlyoutIcon), AttributeHelper.ObjectToDelegate(FlyoutIcon));
            }
            if (FlyoutItemIsVisible != null)
            {
                builder.AddAttribute(nameof(FlyoutItemIsVisible), FlyoutItemIsVisible.Value);
            }
            if (Icon != null)
            {
                builder.AddAttribute(nameof(Icon), AttributeHelper.ObjectToDelegate(Icon));
            }
            if (IsEnabled != null)
            {
                builder.AddAttribute(nameof(IsEnabled), IsEnabled.Value);
            }
            if (IsVisible != null)
            {
                builder.AddAttribute(nameof(IsVisible), IsVisible.Value);
            }
            if (Route != null)
            {
                builder.AddAttribute(nameof(Route), Route);
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
