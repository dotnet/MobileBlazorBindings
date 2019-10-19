using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native
{
    public interface IXamarinFormsElementHandler : IElementHandler
    {
        XF.Element ElementControl { get; }
    }
}
