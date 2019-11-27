using Xamarin.Forms;

namespace Microsoft.Blazor.Native
{
    public interface ICustomParentProcessor
    {
        void SetParent(object parent);
        bool IsParented();
        bool IsParentedTo(Element elementControl);
    }
}
