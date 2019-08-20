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

        public Form RootForm { get; private set; } = new RootForm();

        protected override void InitializeRootAdapter(EmblazonAdapter<Control> adapter)
        {
            // TODO: Might actually want to keep this dummy control so that Blinforms can be an island in a form. But, need
            // to figure out its default size etc. Perhaps top-level Razor class implements ITopLevel{FormSettings} interface
            // to control 'container Form' options?
            adapter.TargetControl = new Control()
            {
                Dock = DockStyle.Fill,
            };

            RootForm.Controls.Add(adapter.TargetControl);
        }

        protected override void HandleException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override EmblazonAdapter<Control> CreateAdapter()
        {
            return new BlontrolAdapter();
        }
    }
}
