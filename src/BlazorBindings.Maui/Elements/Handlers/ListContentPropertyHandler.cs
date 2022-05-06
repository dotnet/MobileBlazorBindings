// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ListContentPropertyHandler<TElementType, TItemType> : IMauiContainerElementHandler, INonChildContainerElement where TItemType : class
    {
        private readonly Func<TElementType, IList<TItemType>> _listPropertyAccessor;
        private IList<TItemType> _propertyItems;

        public ListContentPropertyHandler(Func<TElementType, IList<TItemType>> listPropertyAccessor)
        {
            _listPropertyAccessor = listPropertyAccessor;
        }

        public void SetParent(object parentElement)
        {
            _propertyItems = _listPropertyAccessor((TElementType)parentElement);
        }

        public void Remove()
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            if (!(child is TItemType typedChild))
            {
                throw new NotSupportedException($"Cannot add item of type {child?.GetType().Name} to a {typeof(TItemType)} collection.");
            }

            _propertyItems.Insert(physicalSiblingIndex, typedChild);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            return _propertyItems.IndexOf(child as TItemType);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            _propertyItems.Remove(child as TItemType);
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
