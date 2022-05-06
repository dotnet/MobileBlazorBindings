// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;
using XFD = Xamarin.Forms.DualScreen;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TwoPaneView : Grid
    {
        static TwoPaneView()
        {
            ElementHandlerRegistry.RegisterElementHandler<TwoPaneView>(
                renderer => new TwoPaneViewHandler(renderer, new XFD.TwoPaneView()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? MinTallModeHeight { get; set; }
        [Parameter] public double? MinWideModeWidth { get; set; }
        [Parameter] public XF.GridLength? Pane1Length { get; set; }
        [Parameter] public XF.GridLength? Pane2Length { get; set; }
        [Parameter] public XFD.TwoPaneViewPriority? PanePriority { get; set; }
        [Parameter] public XFD.TwoPaneViewTallModeConfiguration? TallModeConfiguration { get; set; }
        [Parameter] public XFD.TwoPaneViewWideModeConfiguration? WideModeConfiguration { get; set; }

        public new XFD.TwoPaneView NativeControl => ((TwoPaneViewHandler)ElementHandler).TwoPaneViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (MinTallModeHeight != null)
            {
                builder.AddAttribute(nameof(MinTallModeHeight), AttributeHelper.DoubleToString(MinTallModeHeight.Value));
            }
            if (MinWideModeWidth != null)
            {
                builder.AddAttribute(nameof(MinWideModeWidth), AttributeHelper.DoubleToString(MinWideModeWidth.Value));
            }
            if (Pane1Length != null)
            {
                builder.AddAttribute(nameof(Pane1Length), AttributeHelper.GridLengthToString(Pane1Length.Value));
            }
            if (Pane2Length != null)
            {
                builder.AddAttribute(nameof(Pane2Length), AttributeHelper.GridLengthToString(Pane2Length.Value));
            }
            if (PanePriority != null)
            {
                builder.AddAttribute(nameof(PanePriority), (int)PanePriority.Value);
            }
            if (TallModeConfiguration != null)
            {
                builder.AddAttribute(nameof(TallModeConfiguration), (int)TallModeConfiguration.Value);
            }
            if (WideModeConfiguration != null)
            {
                builder.AddAttribute(nameof(WideModeConfiguration), (int)WideModeConfiguration.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
