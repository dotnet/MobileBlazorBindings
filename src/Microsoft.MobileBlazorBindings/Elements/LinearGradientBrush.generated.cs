// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class LinearGradientBrush : GradientBrush
    {
        static LinearGradientBrush()
        {
            ElementHandlerRegistry.RegisterElementHandler<LinearGradientBrush>(
                renderer => new LinearGradientBrushHandler(renderer, new XF.LinearGradientBrush()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public XF.Point? EndPoint { get; set; }
        [Parameter] public XF.Point? StartPoint { get; set; }

        public new XF.LinearGradientBrush NativeControl => ((LinearGradientBrushHandler)ElementHandler).LinearGradientBrushControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (EndPoint != null)
            {
                builder.AddAttribute(nameof(EndPoint), AttributeHelper.PointToString(EndPoint.Value));
            }
            if (StartPoint != null)
            {
                builder.AddAttribute(nameof(StartPoint), AttributeHelper.PointToString(StartPoint.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
