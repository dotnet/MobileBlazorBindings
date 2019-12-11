// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ActivityIndicator : View
    {
        static ActivityIndicator()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<ActivityIndicator>(renderer => new ActivityIndicatorHandler(renderer, new XF.ActivityIndicator()));
        }

        [Parameter] public bool? IsRunning { get; set; }
        [Parameter] public XF.Color? Color { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsRunning != null)
            {
                builder.AddAttribute(nameof(IsRunning), IsRunning.Value);
            }
            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }
        }
    }
}
