using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.DataTemplates
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Class is used as generic parameter.
    internal class DataTemplateItemsComponent<T> : ComponentBase
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, ElementName);

            foreach (var itemRoot in _itemRoots)
            {
                builder.OpenComponent<InitializedContentView>(1);

                builder.AddAttribute(2, nameof(InitializedContentView.NativeControl), itemRoot);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenComponent<DataTemplateItemComponent<T>>(4);
                    builder.AddAttribute(5, nameof(DataTemplateItemComponent<T>.ContentView), itemRoot);
                    builder.AddAttribute(6, nameof(DataTemplateItemComponent<T>.Template), Template);
                    builder.CloseComponent();
                }));

                builder.CloseComponent();
            }

            builder.CloseElement();
        }

        // ElementName is parametrized so that component would be handled by appropriate handler.
        [Parameter] public string ElementName { get; set; }
        [Parameter] public RenderFragment<T> Template { get; set; }

        private readonly List<XF.ContentView> _itemRoots = new List<XF.ContentView>();

        public void Add(XF.ContentView templateRoot)
        {
            _itemRoots.Add(templateRoot);
            StateHasChanged();
        }
    }
}
