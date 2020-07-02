// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class Button : FormsComponentBase
    {
        static Button()
        {
            ElementHandlerRegistry.RegisterElementHandler<Button>(
                renderer => new BlazorButton(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }

            builder.AddAttribute("onclick", OnClick);
        }

        private class BlazorButton : System.Windows.Forms.Button, IWindowsFormsControlHandler
        {
            public BlazorButton(NativeComponentRenderer renderer)
            {
                Click += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(ClickEventHandlerId, null, new EventArgs());
                    }
                };
                Renderer = renderer;
            }

            public ulong ClickEventHandlerId { get; set; }
            public NativeComponentRenderer Renderer { get; }

            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case "onclick":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (ClickEventHandlerId == id) { ClickEventHandlerId = 0; } });
                        ClickEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
