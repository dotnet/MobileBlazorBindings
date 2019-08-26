using Emblazon;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class BlinFormsRenderer : EmblazonRenderer<Control>
    {
        public BlinFormsRenderer(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public Form RootForm { get; } = new RootForm();

        protected override void HandleException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override Control CreateRootControl()
        {
            return RootForm;
        }

        protected override NativeControlManager<Control> CreateNativeControlManager()
        {
            return new BlinFormsNativeControlManager();
        }
    }
}
