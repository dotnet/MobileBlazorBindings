// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.DataTemplates;
using System;
using Xamarin.Forms;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class DataTemplatePropertyHandler<TElementType, TItemType> : IXamarinFormsContainerElementHandler, INonChildContainerElement
    {
        private readonly DataTemplateItemsComponent<TItemType> _dataTemplateItemsComponent;
        private readonly Action<TElementType, DataTemplate> _setPropertyAction;

        public DataTemplatePropertyHandler(IComponent dataTemplateItemsComponent, Action<TElementType, DataTemplate> setPropertyAction)
        {
            _dataTemplateItemsComponent = (DataTemplateItemsComponent<TItemType>)dataTemplateItemsComponent;
            _setPropertyAction = setPropertyAction;
        }

        public void SetParent(object parentElement)
        {
            var parent = (TElementType)parentElement;
            var dataTemplate = new MbbDataTemplate<TItemType>(_dataTemplateItemsComponent);
            _setPropertyAction(parent, dataTemplate);
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        void IXamarinFormsContainerElementHandler.AddChild(XF.Element child, int physicalSiblingIndex) { }

        void IXamarinFormsContainerElementHandler.RemoveChild(XF.Element child) { }

        int IXamarinFormsContainerElementHandler.GetChildIndex(XF.Element child) => -1;

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
