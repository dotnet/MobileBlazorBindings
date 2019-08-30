using Emblazon;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public interface IFormsControlHandler : INativeControlHandler
    {
        Element Element { get; }
    }
}