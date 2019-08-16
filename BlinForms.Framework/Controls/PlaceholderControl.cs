using System;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class PlaceholderControl : Control, ICustomParentingBehavior
    {
        private Control _parentControl;

        public string State { get; set; }

        public Control GetEffectiveParentControl()
        {
            var parentSplitter = (System.Windows.Forms.SplitContainer)_parentControl;
            var containerPanel = State == "Panel1" ? parentSplitter.Panel1 : State == "Panel2" ? parentSplitter.Panel2 : throw new InvalidOperationException("Invalid panel state!!!");
            return containerPanel;
        }

        public void SetActualParentControl(Control parentControl)
        {
            _parentControl = parentControl;
        }
    }
}
