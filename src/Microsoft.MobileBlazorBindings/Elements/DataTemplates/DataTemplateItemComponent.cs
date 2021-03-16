using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.DataTemplates
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Class is used as generic parameter.
    internal class DataTemplateItemComponent<T> : ComponentBase
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        private XF.ContentView _contentView;
        private object _item;

        [Parameter] public RenderFragment<T> Template { get; set; }

        [Parameter]
        public XF.ContentView ContentView
        {
            get
            {
                return _contentView;
            }
            set
            {
                if (_contentView != null && _contentView != value)
                {
                    throw new NotSupportedException("Cannot re-assign ContentView after being originally set.");
                }

                _contentView = value;
                OnContentViewSet();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (_item != null)
            {
                builder.AddContent(0, Template.Invoke((T)_item));
            }
        }

        private void OnContentViewSet()
        {
            _item = ContentView.BindingContext;

            ContentView.BindingContextChanged += (_, __) =>
            {
                if (ContentView.BindingContext != null && _item != ContentView.BindingContext)
                {
                    _item = ContentView.BindingContext;
                    StateHasChanged();
                }
            };
        }
    }
}
