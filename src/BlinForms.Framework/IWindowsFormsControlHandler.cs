using Emblazon;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public interface IWindowsFormsControlHandler : IElementHandlerWithControl
    {
        Control Control { get; }
    }
}
