using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public interface IFormsControlHandler : INativeControlHandler
    {
        XF.Element ElementControl { get; }
    }
}
