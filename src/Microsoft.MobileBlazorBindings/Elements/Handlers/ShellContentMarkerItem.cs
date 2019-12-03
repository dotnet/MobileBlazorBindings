using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    internal sealed class ShellContentMarkerItem : XF.FlyoutItem
    {
        public ShellContentMarkerItem()
        {
            // Set dummy content to ensure the item is valid
            var tab = new XF.Tab();
            tab.Items.Add(new XF.ShellContent
            {
                Content = new XF.ContentPage()
            });
            Items.Add(tab);
        }
    }
}
