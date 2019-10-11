using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{

    namespace Handlers
    {
        public class ModalContainerHandler : IXamarinFormsElementHandler
        {
            private XF.NavigableElement _modalParent;
            private XF.Page _modalChild;

            public ModalContainerHandler(EmblazonRenderer renderer, ModalContainerPlaceholderElement modalContainerPlaceholderElementControl)
            {
                Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
                ModalContainerPlaceholderElementControl = modalContainerPlaceholderElementControl ?? throw new ArgumentNullException(nameof(modalContainerPlaceholderElementControl));
            }

            public EmblazonRenderer Renderer { get; }
            public XF.Element ModalContainerPlaceholderElementControl { get; }
            public XF.Element ElementControl => ModalContainerPlaceholderElementControl;
            public object TargetElement => ElementControl;

            public XF.NavigableElement ModalParent
            {
                get => _modalParent;
                set
                {
                    _modalParent = value;
                    ShowDialogIfPossible();
                }
            }
            public XF.Page ModalChild
            {
                get => _modalChild;
                set
                {
                    _modalChild = value;
                    ShowDialogIfPossible();
                }
            }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case "__ShowDialog":
                        ShowDialogIfPossible();
                        break;
                    default:
                        throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
                }
            }

            private void ShowDialogIfPossible()
            {
                if (ModalParent != null && ModalChild != null)
                {
                    ModalParent.Navigation.PushModalAsync(ModalChild);
                }
            }
        }
    }
}
