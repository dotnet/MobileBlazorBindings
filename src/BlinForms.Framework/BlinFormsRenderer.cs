using Emblazon;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class BlinFormsRenderer : EmblazonRenderer<IWindowsFormsControlHandler>
    {
        public BlinFormsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        protected override void HandleException(Exception exception)
        {
            MessageBox.Show(exception?.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override ElementManager<IWindowsFormsControlHandler> CreateNativeControlManager()
        {
            return new BlinFormsElementManager();
        }
    }
}
