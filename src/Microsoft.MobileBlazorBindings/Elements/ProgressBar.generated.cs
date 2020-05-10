// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ProgressBar : View
    {
        static ProgressBar()
        {
            ElementHandlerRegistry.RegisterElementHandler<ProgressBar>(
                renderer => new ProgressBarHandler(renderer, new XF.ProgressBar()));
        }

        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        /// <value>
        /// Gets or sets a value that specifies the fraction of the bar that is colored.
        /// </value>
        [Parameter] public double? Progress { get; set; }
        /// <summary>
        /// Get or sets the color of the progress bar.
        /// </summary>
        /// <value>
        /// The color of the progress bar.
        /// </value>
        [Parameter] public XF.Color? ProgressColor { get; set; }

        public new XF.ProgressBar NativeControl => ((ProgressBarHandler)ElementHandler).ProgressBarControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Progress != null)
            {
                builder.AddAttribute(nameof(Progress), AttributeHelper.DoubleToString(Progress.Value));
            }
            if (ProgressColor != null)
            {
                builder.AddAttribute(nameof(ProgressColor), AttributeHelper.ColorToString(ProgressColor.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
