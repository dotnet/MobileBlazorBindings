using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public interface IParentChildManager
    {
        void SetParent(XF.Element parentElement);
        void SetChild(XF.Element childElement);
    }
}
