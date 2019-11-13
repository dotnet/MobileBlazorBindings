using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Blazor.Native.Elements.Handlers;
using System.Collections;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class CollectionView : View
    {
        static CollectionView()
        {
            ElementHandlerRegistry.RegisterElementHandler<CollectionView>(
                renderer => new CollectionViewHandler(renderer, new XF.CollectionView()));
        }

        [Parameter] public RenderFragment<object> ItemTemplate { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        protected override RenderFragment GetChildContent() => RenderChildContent;
#pragma warning restore CA1721 // Property names should not match get methods

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            if (ElementHandler is CollectionViewHandler collectionViewHandler)
            {
                for (int i = 0; i < collectionViewHandler.CollectionViewDataTracker.Data.Count; i++)
                {
                    var data = collectionViewHandler.CollectionViewDataTracker.Data[i];

                    builder.OpenComponent<CollectionViewItemContainer>(i);
                    builder.AddAttribute(0, nameof(CollectionViewItemContainer.DataIndex), data.Item1);
                    builder.AddContent(1, ItemTemplate, data.Item2);
                    builder.CloseComponent();
                }
            }
        }

        public void SetItemsSource(IEnumerable data)
        {
            var collectionViewHandler = (CollectionViewHandler)ElementHandler;
            //collectionViewHandler.CollectionViewDataTracker.Data.Clear();

            collectionViewHandler.CollectionViewDataTracker.DataChanged += OnCollectionViewDataTrackerDataChanged;

            collectionViewHandler.CollectionViewControl.ItemsSource = data;
        }

        private void OnCollectionViewDataTrackerDataChanged(object sender, System.EventArgs e)
        {
            StateHasChanged();
        }
    }
}
