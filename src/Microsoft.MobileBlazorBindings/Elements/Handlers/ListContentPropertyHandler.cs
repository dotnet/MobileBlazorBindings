// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ListContentPropertyHandler<TElementType, TItemType> : IXamarinFormsContainerElementHandler, INonChildContainerElement where TItemType : XF.Element
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

        void IXamarinFormsContainerElementHandler.AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (!(child is TItemType typedChild))
            {
                throw new NotSupportedException($"Cannot add item of type {child?.GetType().Name} to a {typeof(TItemType)} collection.");
            }

            _propertyItems.Insert(physicalSiblingIndex, typedChild);
        }

        int IXamarinFormsContainerElementHandler.GetChildIndex(XF.Element child)
        {
            return _propertyItems.IndexOf(child as TItemType);
        }

        void IXamarinFormsContainerElementHandler.RemoveChild(XF.Element child)
        {
            _propertyItems.Remove(child as TItemType);
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }

        XF.Element IXamarinFormsElementHandler.ElementControl => null;
        bool IXamarinFormsElementHandler.IsParented() => false;
        bool IXamarinFormsElementHandler.IsParentedTo(XF.Element parent) => false;

        void IXamarinFormsElementHandler.SetParent(XF.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }
    }
}
