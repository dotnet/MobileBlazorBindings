// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ElementHandler : IMauiElementHandler
    {
        private readonly EventManager _eventManager = new EventManager();

        public ElementHandler(NativeComponentRenderer renderer, MC.Element elementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ElementControl = elementControl ?? throw new ArgumentNullException(nameof(elementControl));
        }

        protected void ConfigureEvent(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            _eventManager.ConfigureEvent(eventName, setId, clearId);
        }

        public NativeComponentRenderer Renderer { get; }
        public MC.Element ElementControl { get; }
        public object TargetElement => ElementControl;

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Element.AutomationId):
                    ElementControl.AutomationId = (string)attributeValue;
                    break;
                case nameof(MC.Element.ClassId):
                    ElementControl.ClassId = (string)attributeValue;
                    break;
                case nameof(MC.Element.StyleId):
                    ElementControl.StyleId = (string)attributeValue;
                    break;
                default:
                    if (!ApplyAdditionalAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName) &&
                        !_eventManager.TryRegisterEvent(Renderer, attributeName, attributeEventHandlerId))
                    {
                        throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
                    }
                    break;
            }
        }

        public virtual bool ApplyAdditionalAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            return false;
        }

        public virtual bool IsParented()
        {
            return ElementControl.Parent != null;
        }

        public virtual bool IsParentedTo(MC.Element parent)
        {
            return ElementControl.Parent == parent;
        }

        public virtual void SetParent(MC.Element parent)
        {
            ElementControl.Parent = parent;
        }
    }
}
