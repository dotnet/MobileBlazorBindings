// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ElementHandler : IXamarinFormsElementHandler
    {
        public ElementHandler(NativeComponentRenderer renderer, XF.Element elementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ElementControl = elementControl ?? throw new ArgumentNullException(nameof(elementControl));
        }

        protected void RegisterEvent(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            RegisteredEvents[eventName] = new EventRegistration(eventName, setId, clearId);
        }
        private Dictionary<string, EventRegistration> RegisteredEvents { get; } = new Dictionary<string, EventRegistration>();

        public NativeComponentRenderer Renderer { get; }
        public XF.Element ElementControl { get; }
        public object TargetElement => ElementControl;

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Element.AutomationId):
                    ElementControl.AutomationId = (string)attributeValue;
                    break;
                case nameof(XF.Element.ClassId):
                    ElementControl.ClassId = (string)attributeValue;
                    break;
                case nameof(XF.Element.StyleId):
                    ElementControl.StyleId = (string)attributeValue;
                    break;
                default:
                    if (!TryRegisterEvent(attributeName, attributeEventHandlerId))
                    {
                        throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
                    }
                    break;
            }
        }

        private bool TryRegisterEvent(string eventName, ulong eventHandlerId)
        {
            if (RegisteredEvents.TryGetValue(eventName, out var eventRegistration))
            {
                Renderer.RegisterEvent(eventHandlerId, eventRegistration.ClearId);
                eventRegistration.SetId(eventHandlerId);

                return true;
            }
            return false;
        }
    }
}
