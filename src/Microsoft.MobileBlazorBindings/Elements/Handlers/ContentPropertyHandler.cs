// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ContentPropertyHandler<TElementType> : IXamarinFormsContainerElementHandler, INonChildContainerElement
    {
        private readonly Action<TElementType, XF.Element> _setPropertyAction;
        private TElementType _parent;

        public ContentPropertyHandler(Action<TElementType, XF.Element> setPropertyAction)
        {
            _setPropertyAction = setPropertyAction;
        }

        public void SetParent(object parentElement)
        {
            _parent = (TElementType)parentElement;
        }

        void IXamarinFormsContainerElementHandler.AddChild(XF.Element child, int physicalSiblingIndex)
        {
            _setPropertyAction(_parent, child);
        }

        int IXamarinFormsContainerElementHandler.GetChildIndex(XF.Element child)
        {
            return -1;
        }

        void IXamarinFormsContainerElementHandler.RemoveChild(XF.Element child)
        {
            _setPropertyAction(_parent, null);
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
