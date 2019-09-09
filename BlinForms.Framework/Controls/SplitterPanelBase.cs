using Microsoft.AspNetCore.Components;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public abstract class SplitterPanelBase : FormsComponentBase
    {
        private protected static IWindowsFormsControlHandler GetSplitterPanel(Control parentControl, int panelNumber)
        {
            if (!(parentControl is System.Windows.Forms.SplitContainer splitContainer))
            {
                // This gets called from a static constructor, so we really don't want to throw from here
                return null;
            }
            return panelNumber switch
            {
                1 => new BlazorSplitterPanelWrapper(splitContainer.Panel1),
                2 => new BlazorSplitterPanelWrapper(splitContainer.Panel2),
                _ => throw new InvalidOperationException($"Invalid SplitContainer panel number: {panelNumber}. The only valid values are '1' and '2'."),
            };
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        private sealed class BlazorSplitterPanelWrapper : IWindowsFormsControlHandler
        {
            public BlazorSplitterPanelWrapper(SplitterPanel splitterPanel)
            {
                SplitterPanel = splitterPanel;
            }

            public Control Control => SplitterPanel;

            public object NativeControl => SplitterPanel;

            public SplitterPanel SplitterPanel { get; }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                FormsComponentBase.ApplyAttribute(SplitterPanel, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
