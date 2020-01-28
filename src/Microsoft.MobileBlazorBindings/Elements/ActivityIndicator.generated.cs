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

        [Parameter] public XF.Color? Color { get; set; }
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
