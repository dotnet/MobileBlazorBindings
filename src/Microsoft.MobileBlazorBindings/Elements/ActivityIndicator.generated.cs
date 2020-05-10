// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ActivityIndicator : View
    {
        static ActivityIndicator()
        {
            ElementHandlerRegistry.RegisterElementHandler<ActivityIndicator>(
                renderer => new ActivityIndicatorHandler(renderer, new XF.ActivityIndicator()));
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> of the ActivityIndicator. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.Color" /> used to display the ActivityIndicator. Default is <see cref="P:Xamarin.Forms.Color.Default" />.
        /// </value>
        [Parameter] public XF.Color? Color { get; set; }
        /// <summary>
        /// Gets or sets the value indicating if the ActivityIndicator is running. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.Boolean" /> indicating if the ActivityIndicator is running.
        /// </value>
        [Parameter] public bool? IsRunning { get; set; }

        public new XF.ActivityIndicator NativeControl => ((ActivityIndicatorHandler)ElementHandler).ActivityIndicatorControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }
            if (IsRunning != null)
            {
                builder.AddAttribute(nameof(IsRunning), IsRunning.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
