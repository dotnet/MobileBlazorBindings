using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public interface IXamarinFormsElementHandler : IElementHandlerWithControl
    {
        XF.Element ElementControl { get; }
    }
}
