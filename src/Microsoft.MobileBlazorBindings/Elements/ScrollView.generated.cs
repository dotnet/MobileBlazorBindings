// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ScrollView : Layout
    {
        static ScrollView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ScrollView>(
                renderer => new ScrollViewHandler(renderer, new XF.ScrollView()));
        }

        /// <summary>
        /// Gets or sets a value that controls when the horizontal scroll bar is visible.
        /// </summary>
        /// <value>
        /// A value that controls when the horizontal scroll bar is visible.
        /// </value>
        [Parameter] public XF.ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        /// <summary>
        /// Gets or sets the scrolling direction of the ScrollView. This is a bindable property.
        /// </summary>
        [Parameter] public XF.ScrollOrientation? Orientation { get; set; }
        /// <summary>
        /// Gets or sets a value that controls when the vertical scroll bar is visible.
        /// </summary>
        /// <value>
        /// A value that controls when the vertical scroll bar is visible.
        /// </value>
        [Parameter] public XF.ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        public new XF.ScrollView NativeControl => ((ScrollViewHandler)ElementHandler).ScrollViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(HorizontalScrollBarVisibility), (int)HorizontalScrollBarVisibility.Value);
            }
            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (VerticalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(VerticalScrollBarVisibility), (int)VerticalScrollBarVisibility.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
