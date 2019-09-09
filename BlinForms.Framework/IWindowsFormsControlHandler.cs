using Emblazon;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public interface IWindowsFormsControlHandler : INativeControlHandler
    {
        Control Control { get; }
    }
}
