// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ModalContainerHandler : IXamarinFormsContainerElementHandler, INonChildContainerElement
    {
        public ModalContainerHandler(NativeComponentRenderer renderer, DummyElement modalContainerDummyControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ModalContainerPlaceholderElementControl = modalContainerDummyControl ?? throw new ArgumentNullException(nameof(modalContainerDummyControl));

            _parentChildManager = new ParentChildManager<XF.NavigableElement, XF.Page>(ShowDialogIfPossible);
            _parentChildManager.ChildChanged += OnParentChildManagerChildChanged;
        }

        public NativeComponentRenderer Renderer { get; }
        public DummyElement ModalContainerPlaceholderElementControl { get; }
        public XF.Element ElementControl => ModalContainerPlaceholderElementControl;
        public object TargetElement => ElementControl;

        private readonly ParentChildManager<XF.NavigableElement, XF.Page> _parentChildManager;

        private void OnParentChildManagerChildChanged(object sender, EventArgs e)
        {
            if (_parentChildManager.Child != null)
            {
                _parentChildManager.Child.Disappearing += CleanUpDisappearingPage;
            }
        }

        private void CleanUpDisappearingPage(object sender, EventArgs e)
        {
            _parentChildManager.Child.Disappearing -= CleanUpDisappearingPage;
            _parentChildManager.Parent = null;
            _parentChildManager.Child = null;

            if (ClosedEventHandlerId != default)
            {
                Renderer.Dispatcher.InvokeAsync(() => Renderer.DispatchEventAsync(ClosedEventHandlerId, null, e));
            }
        }

        public ulong ClosedEventHandlerId { get; set; }

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case "__ShowDialog":
                    if (attributeValue != null)
                    {
                        ShowDialogIfPossible(_parentChildManager);
                    }
                    break;
                case "onclosed":
                    Renderer.RegisterEvent(attributeEventHandlerId, id => { if (ClosedEventHandlerId == id) { ClosedEventHandlerId = 0; } });
                    ClosedEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        private void ShowDialogIfPossible(ParentChildManager<XF.NavigableElement, XF.Page> parentChildManager)
        {
            if (_parentChildManager.Parent != null && _parentChildManager.Child != null)
            {
                _parentChildManager.Parent.Navigation.PushModalAsync(_parentChildManager.Child);
            }
        }

        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            _parentChildManager.SetChild(child);
        }

        public void RemoveChild(XF.Element child)
        {
            // TODO: This could probably be implemented at some point, but it isn't needed right now
            throw new NotImplementedException();
        }

        public bool IsParented()
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public bool IsParentedTo(XF.Element parent)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public void SetParent(XF.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }

        public int GetChildIndex(XF.Element child)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return 0;
        }

        public void SetParent(object parentElement)
        {
            _parentChildManager.SetParent((XF.Element)parentElement);
        }
    }
}
