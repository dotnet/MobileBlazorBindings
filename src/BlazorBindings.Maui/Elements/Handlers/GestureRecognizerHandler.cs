// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class GestureRecognizerHandler : IMauiElementHandler, INonChildContainerElement
    {
        private readonly EventManager _eventManager = new EventManager();
        private object _parentElement;

        public GestureRecognizerHandler(NativeComponentRenderer renderer, MC.GestureRecognizer gestureRecognizerControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            GestureRecognizerControl = gestureRecognizerControl ?? throw new ArgumentNullException(nameof(gestureRecognizerControl));
        }

        protected void ConfigureEvent(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            _eventManager.ConfigureEvent(eventName, setId, clearId);
        }

        public NativeComponentRenderer Renderer { get; }
        public MC.GestureRecognizer GestureRecognizerControl { get; }
        public MC.Element ElementControl => GestureRecognizerControl;
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

        public bool IsParentedTo(MC.Element parent)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public void SetParent(MC.Element parent)
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

            _parentElement = parentElement;

            switch (_parentElement)
            {
                case MC.View view:
                    view.GestureRecognizers.Add(GestureRecognizerControl);
                    break;
                case MC.GestureElement gestureElement:
                    gestureElement.GestureRecognizers.Add(GestureRecognizerControl);
                    break;
                default:
                    throw new InvalidOperationException($"Gesture of type {ElementControl.GetType().FullName} can't be added to parent of type {parentElement.GetType().FullName}.");
            }
        }

        public void Remove()
        {
            switch (_parentElement)
            {
                case MC.View view:
                    view.GestureRecognizers.Remove(GestureRecognizerControl);
                    break;
                case MC.GestureElement gestureElement:
                    gestureElement.GestureRecognizers.Remove(GestureRecognizerControl);
                    break;
                default:
                    throw new InvalidOperationException($"Gesture of type {ElementControl.GetType().FullName} can't be removed from parent of type {_parentElement.GetType().FullName}.");
            }
        }
    }
}
