// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TabbedPage : Page
    {
        static TabbedPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<TabbedPage>(
                renderer => new TabbedPageHandler(renderer, new MC.TabbedPage()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BarBackgroundColor { get; set; }
        [Parameter] public Color BarTextColor { get; set; }
        [Parameter] public Color SelectedTabColor { get; set; }
        [Parameter] public Color UnselectedTabColor { get; set; }

        public new MC.TabbedPage NativeControl => ((TabbedPageHandler)ElementHandler).TabbedPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BarBackgroundColor != null)
            {
                builder.AddAttribute(nameof(BarBackgroundColor), AttributeHelper.ColorToString(BarBackgroundColor));
            }
            if (BarTextColor != null)
            {
                builder.AddAttribute(nameof(BarTextColor), AttributeHelper.ColorToString(BarTextColor));
            }
            if (SelectedTabColor != null)
            {
                builder.AddAttribute(nameof(SelectedTabColor), AttributeHelper.ColorToString(SelectedTabColor));
            }
            if (UnselectedTabColor != null)
            {
                builder.AddAttribute(nameof(UnselectedTabColor), AttributeHelper.ColorToString(UnselectedTabColor));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
