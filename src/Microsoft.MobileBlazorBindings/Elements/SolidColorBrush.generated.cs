// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class SolidColorBrush : Brush
    {
        static SolidColorBrush()
        {
            ElementHandlerRegistry.RegisterElementHandler<SolidColorBrush>(
                renderer => new SolidColorBrushHandler(renderer, new XF.SolidColorBrush()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public XF.Color? Color { get; set; }

        public new XF.SolidColorBrush NativeControl => ((SolidColorBrushHandler)ElementHandler).SolidColorBrushControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
