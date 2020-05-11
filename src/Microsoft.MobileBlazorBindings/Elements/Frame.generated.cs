// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Frame : ContentView
    {
        static Frame()
        {
            ElementHandlerRegistry.RegisterElementHandler<Frame>(
                renderer => new FrameHandler(renderer, new XF.Frame()));
        }

        /// <summary>
        /// Gets or sets the border color for the frame.
        /// </summary>
        /// <value>
        /// The border color for the frame.
        /// </value>
        [Parameter] public XF.Color? BorderColor { get; set; }
        /// <summary>
        /// Gets or sets the corner radius of the frame.
        /// </summary>
        [Parameter] public float? CornerRadius { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating if the Frame has a shadow displayed. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.Boolean" /> indicating whether or not the Frame has a shadow. Default is <see langword="true" />.
        /// </value>
        [Parameter] public bool? HasShadow { get; set; }

        public new XF.Frame NativeControl => ((FrameHandler)ElementHandler).FrameControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.SingleToString(CornerRadius.Value));
            }
            if (HasShadow != null)
            {
                builder.AddAttribute(nameof(HasShadow), HasShadow.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
