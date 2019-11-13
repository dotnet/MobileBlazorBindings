using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class CollectionViewItemContainerHandler : IXamarinFormsElementHandler, INonChildContainerElement, IParentChildManagementRequired
    {
        public CollectionViewItemContainerHandler(EmblazonRenderer renderer, DummyElement modalContainerDummyControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ModalContainerPlaceholderElementControl = modalContainerDummyControl ?? throw new ArgumentNullException(nameof(modalContainerDummyControl));

            // Note: We *say* the parent is CollectionView because that's the parent in the Razor markup,
            // but when it comes time to set the actual parent for the child, the real container within the
            // CollectionView is used
            ParentChildManager = new ParentChildManager<XF.CollectionView, XF.View>(SetCollectionViewItemContent);
        }

        public EmblazonRenderer Renderer { get; }
        public DummyElement ModalContainerPlaceholderElementControl { get; }
        public XF.Element ElementControl => ModalContainerPlaceholderElementControl;
        public object TargetElement => ElementControl;

        public IParentChildManager ParentChildManager { get; }

        private XF.ContentView _realParent;

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(CollectionViewItemContainer.DataIndex):
                    // Once we know our data index we know everything
                    var dataIndex = AttributeHelper.GetInt(attributeValue);
                    _realParent = CollectionViewDataTracker.Stuff[dataIndex];
                    break;
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        private void SetCollectionViewItemContent(ParentChildManager<XF.CollectionView, XF.View> parentChildManager)
        {
            //_realParent.Content = parentChildManager.Child;
        }
    }
}
