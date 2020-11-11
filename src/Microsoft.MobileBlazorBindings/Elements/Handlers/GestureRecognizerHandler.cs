// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GestureRecognizerHandler : IXamarinFormsElementHandler, INonChildContainerElement
    {
        private readonly EventManager _eventManager = new EventManager();

        public GestureRecognizerHandler(NativeComponentRenderer renderer, XF.GestureRecognizer gestureRecognizerControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            GestureRecognizerControl = gestureRecognizerControl ?? throw new ArgumentNullException(nameof(gestureRecognizerControl));
        }

        protected void ConfigureEvent(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            _eventManager.ConfigureEvent(eventName, setId, clearId);
        }

        public NativeComponentRenderer Renderer { get; }
        public XF.GestureRecognizer GestureRecognizerControl { get; }
        public XF.Element ElementControl => GestureRecognizerControl;
        public object TargetElement => GestureRecognizerControl;

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    if (!_eventManager.TryRegisterEvent(Renderer, attributeName, attributeEventHandlerId))
                    {
                        throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
                    }
                    break;
            }
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

        public void SetParent(object parentElement)
        {
            if (parentElement is null)
            {
                throw new ArgumentNullException(nameof(parentElement));
            }

            switch (parentElement)
            {
                case XF.View view:
                    view.GestureRecognizers.Add(GestureRecognizerControl);
                    break;
                case XF.GestureElement gestureElement:
                    gestureElement.GestureRecognizers.Add(GestureRecognizerControl);
                    break;
                default:
                    throw new InvalidOperationException($"Gesture of type {ElementControl.GetType().FullName} can't be added to parent of type {parentElement.GetType().FullName}.");
            }
        }
    }
}
