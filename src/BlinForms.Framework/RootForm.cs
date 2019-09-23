using System.Windows.Forms;

namespace BlinForms.Framework
{
    public partial class RootForm : Form, IWindowsFormsControlHandler
    {
        public RootForm()
        {
            InitializeComponent();
        }

        public Control Control => this;
        public object NativeControl => this;

        // TODO: Need to think about whether this method is needed. There's no component for this element, so when
        // would this get called?
        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            global::BlinForms.Framework.Controls.FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }
    }
}
