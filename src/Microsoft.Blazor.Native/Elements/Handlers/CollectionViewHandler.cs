using Emblazon;
using System;
using System.Collections.Generic;
using System.Threading;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class CollectionViewDataTracker
    {
        public event EventHandler DataChanged;
        public static Dictionary<int, XF.ContentView> Stuff { get; } = new Dictionary<int, XF.ContentView>();
        private static volatile int _counter;

        public List<(int, object)> Data { get; } = new List<(int, object)>();

        public void AddData(object data, XF.ContentView contentContainer)
        {
            var currentCounter = Interlocked.Increment(ref _counter);
            Stuff[currentCounter] = contentContainer;
            Data.Add((currentCounter, data));
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class CollectionViewHandler : ViewHandler
    {
        public CollectionViewHandler(EmblazonRenderer renderer, XF.CollectionView collectionViewControl) : base(renderer, collectionViewControl)
        {
            CollectionViewControl = collectionViewControl ?? throw new ArgumentNullException(nameof(collectionViewControl));
            collectionViewControl.ItemTemplate = new XF.DataTemplate(() => new MyDataTemplate(CollectionViewDataTracker));
        }

        public CollectionViewDataTracker CollectionViewDataTracker { get; } = new CollectionViewDataTracker();

        public XF.CollectionView CollectionViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private class MyDataTemplate : XF.ContentView
        {
            private readonly CollectionViewDataTracker _collectionViewDataTracker;

            public MyDataTemplate(CollectionViewDataTracker collectionViewDataTracker)
            {
                _collectionViewDataTracker = collectionViewDataTracker;
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                _collectionViewDataTracker.AddData(BindingContext, this);
                // TODO: Send BindingContext value to the object's "Data"?? property, then call StateHasChanged?
                Content = new XF.Label
                {
                    Text = "I am not yet templated...",
                };
            }
        }
    }
}
