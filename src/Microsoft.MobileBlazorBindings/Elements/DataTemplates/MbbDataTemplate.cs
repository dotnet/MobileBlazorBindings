using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.DataTemplates
{
    internal class MbbDataTemplate<T> : XF.DataTemplate
    {
        public MbbDataTemplate(DataTemplateItemsComponent<T> dataTemplateItemsAccessor)
            : base(() =>
            {
                var contentView = new XF.ContentView();
                dataTemplateItemsAccessor.Add(contentView);
                return contentView;
            })
        {
        }
    }
}
