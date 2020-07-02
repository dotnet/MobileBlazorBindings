// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class TextBox : FormsComponentBase
    {
        static TextBox()
        {
            ElementHandlerRegistry.RegisterElementHandler<TextBox>(renderer => new BlazorTextBox(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool? Multiline { get; set; }
        [Parameter] public bool? ReadOnly { get; set; }
        [Parameter] public bool? WordWrap { get; set; }
        [Parameter] public ScrollBars? ScrollBars { get; set; }

        [Parameter] public EventCallback<string> TextChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Multiline != null)
            {
                builder.AddAttribute(nameof(Multiline), Multiline.Value);
            }
            if (ReadOnly != null)
            {
                builder.AddAttribute(nameof(ReadOnly), ReadOnly.Value);
            }
            if (WordWrap != null)
            {
                builder.AddAttribute(nameof(WordWrap), WordWrap.Value);
            }
            if (ScrollBars != null)
            {
                builder.AddAttribute(nameof(ScrollBars), (int)ScrollBars.Value);
            }

            builder.AddAttribute("ontextchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleTextChanged));
        }

        private Task HandleTextChanged(ChangeEventArgs evt)
        {
            return TextChanged.InvokeAsync((string)evt.Value);
        }

        private class BlazorTextBox : System.Windows.Forms.TextBox, IWindowsFormsControlHandler
        {
            public BlazorTextBox(NativeComponentRenderer renderer)
            {
                TextChanged += (s, e) =>
                {
                    if (TextChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = Text });
                    }
                };
                Renderer = renderer;
            }

            public ulong TextChangedEventHandlerId { get; set; }
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
                    case nameof(Multiline):
                        Multiline = AttributeHelper.GetBool(attributeValue);
                        break;
                    case nameof(ReadOnly):
                        ReadOnly = AttributeHelper.GetBool(attributeValue);
                        break;
                    case nameof(WordWrap):
                        WordWrap = AttributeHelper.GetBool(attributeValue);
                        break;
                    case nameof(ScrollBars):
                        ScrollBars = (ScrollBars)AttributeHelper.GetInt(attributeValue);
                        break;
                    case "ontextchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (TextChangedEventHandlerId == id) { TextChangedEventHandlerId = 0; } });
                        TextChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
