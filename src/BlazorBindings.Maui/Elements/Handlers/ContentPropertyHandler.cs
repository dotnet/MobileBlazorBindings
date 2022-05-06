// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ContentPropertyHandler<TElementType> : IMauiContainerElementHandler, INonChildContainerElement
    {
        private readonly Action<TElementType, MC.Element> _setPropertyAction;
        private TElementType _parent;

        public ContentPropertyHandler(Action<TElementType, MC.Element> setPropertyAction)
        {
            _setPropertyAction = setPropertyAction;
        }

        public void SetParent(object parentElement)
        {
            _parent = (TElementType)parentElement;
        }

        public void Remove()
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            _setPropertyAction(_parent, child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            return -1;
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            _setPropertyAction(_parent, null);
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }

        MC.Element IMauiElementHandler.ElementControl => null;
        bool IMauiElementHandler.IsParented() => false;
        bool IMauiElementHandler.IsParentedTo(MC.Element parent) => false;

        void IMauiElementHandler.SetParent(MC.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }
    }
}
