// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class CheckBox : FormsComponentBase
    {
        static CheckBox()
        {
            ElementHandlerRegistry.RegisterElementHandler<CheckBox>(renderer => new BlazorCheckBox(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool? Checked { get; set; }
        [Parameter] public CheckState? CheckState { get; set; }
        [Parameter] public bool? ThreeState { get; set; }
        [Parameter] public EventCallback<bool> CheckedChanged { get; set; }
        [Parameter] public EventCallback<CheckState> CheckStateChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Checked != null)
            {
                builder.AddAttribute(nameof(Checked), Checked.Value);
            }
            if (CheckState != null)
            {
                builder.AddAttribute(nameof(CheckState), (int)CheckState.Value);
            }
            if (ThreeState != null)
            {
                builder.AddAttribute(nameof(ThreeState), ThreeState.Value);
            }

            builder.AddAttribute("oncheckedchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleCheckedChanged));
            builder.AddAttribute("oncheckstatechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleCheckStateChanged));
        }

        private Task HandleCheckedChanged(ChangeEventArgs evt)
        {
            return CheckedChanged.InvokeAsync((bool)evt.Value);
        }

        private Task HandleCheckStateChanged(ChangeEventArgs evt)
        {
            return CheckStateChanged.InvokeAsync((CheckState)evt.Value);
        }

        private class BlazorCheckBox : System.Windows.Forms.CheckBox, IWindowsFormsControlHandler
        {
            public BlazorCheckBox(NativeComponentRenderer renderer)
            {
                CheckedChanged += (s, e) =>
                {
                    if (CheckedChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(CheckedChangedEventHandlerId, null, new ChangeEventArgs { Value = Checked });
                    }
                };
                CheckStateChanged += (s, e) =>
                {
                    if (CheckStateChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(CheckStateChangedEventHandlerId, null, new ChangeEventArgs { Value = CheckState });
                    }
                };
                Renderer = renderer;
            }

            public ulong CheckedChangedEventHandlerId { get; set; }
            public ulong CheckStateChangedEventHandlerId { get; set; }
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
                    case nameof(Checked):
                        Checked = AttributeHelper.GetBool(attributeValue);
                        break;
                    case nameof(CheckState):
                        CheckState = (CheckState)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(ThreeState):
                        ThreeState = AttributeHelper.GetBool(attributeValue);
                        break;
                    case "oncheckedchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (CheckedChangedEventHandlerId == id) { CheckedChangedEventHandlerId = 0; } });
                        CheckedChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    case "oncheckstatechanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (CheckStateChangedEventHandlerId == id) { CheckStateChangedEventHandlerId = 0; } });
                        CheckStateChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
