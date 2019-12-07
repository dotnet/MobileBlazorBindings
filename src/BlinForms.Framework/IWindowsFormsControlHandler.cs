using Microsoft.MobileBlazorBindings.Core;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public interface IWindowsFormsControlHandler : IElementHandler
    {
        Control Control { get; }
    }
}
