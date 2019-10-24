using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    internal sealed class ShellContentMarkerItem : XF.ShellContent
    {
        public ShellContentMarkerItem()
        {
            // Set dummy content to ensure the item is valid
            Content = new XF.ContentPage();
        }
    }
}
