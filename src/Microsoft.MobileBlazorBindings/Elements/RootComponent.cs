using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using System.Collections.Generic;
using WVM = Microsoft.AspNetCore.Components.WebView.Maui;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class RootComponent : NativeControlComponentBase
    {
        static RootComponent()
        {
            ElementHandlerRegistry.RegisterElementHandler<RootComponent>(
                _ => new RootComponentHandler(new WVM.RootComponent()));
        }

        [Parameter] public string Selector { get; set; }

        [Parameter] public Type ComponentType { get; set; }

        [Parameter] public IDictionary<string, object> Parameters { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Selector != null)
            {
                builder.AddAttribute(nameof(Selector), Selector);
            }
            if (ComponentType != null)
            {
                builder.AddAttribute(nameof(ComponentType), AttributeHelper.ObjectToDelegate(ComponentType));
            }
            if (Parameters != null)
            {
                builder.AddAttribute(nameof(Parameters), AttributeHelper.ObjectToDelegate(Parameters));
            }
        }
    }
}
