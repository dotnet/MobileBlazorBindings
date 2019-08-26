using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class CheckBox : FormsComponentBase
    {
        static CheckBox()
        {
            NativeControlRegistry<System.Windows.Forms.Control>.RegisterNativeControlComponent<CheckBox>(
                renderer => new BlazorCheckBox(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool? Checked { get; set; }
        [Parameter] public CheckState? CheckState { get; set; }
        [Parameter] public bool? ThreeState { get; set; }
        [Parameter] public EventCallback OnCheckedChanged { get; set; }
        [Parameter] public EventCallback OnCheckStateChanged { get; set; }

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

            builder.AddAttribute("oncheckedchanged", OnCheckedChanged);
            builder.AddAttribute("oncheckstatechanged", OnCheckStateChanged);
        }

        class BlazorCheckBox : System.Windows.Forms.CheckBox, IBlazorNativeControl
        {
            public BlazorCheckBox(EmblazonRenderer<System.Windows.Forms.Control> renderer)
            {
                CheckedChanged += (s, e) =>
                {
                    if (CheckedChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(CheckedChangedEventHandlerId, null, new UIEventArgs());
                    }
                };
                CheckStateChanged += (s, e) =>
                {
                    if (CheckStateChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(CheckStateChangedEventHandlerId, null, new UIEventArgs());
                    }
                };
            }

            public ulong CheckedChangedEventHandlerId { get; set; }
            public ulong CheckStateChangedEventHandlerId { get; set; }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case nameof(Checked):
                        Checked = (bool)attributeValue;
                        break;
                    case nameof(CheckState):
                        CheckState = (CheckState)int.Parse((string)attributeValue);
                        break;
                    case nameof(ThreeState):
                        ThreeState = (bool)attributeValue;
                        break;
                    case "oncheckedchanged":
                        CheckedChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    case "oncheckstatechanged":
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
