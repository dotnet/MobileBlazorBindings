using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public interface IXamarinFormsElementHandler : IElementHandler
    {
        XF.Element ElementControl { get; }
    }
}
