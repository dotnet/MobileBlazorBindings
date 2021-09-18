using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements.DataTemplates
{
    internal class MbbDataTemplate<T> : MC.DataTemplate
    {
        public MbbDataTemplate(DataTemplateItemsComponent<T> dataTemplateItemsAccessor)
            : base(() =>
            {
                var contentView = new MC.ContentView();
                dataTemplateItemsAccessor.Add(contentView);
                return contentView;
            })
        {
        }
    }
}
