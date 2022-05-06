// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.DataTemplates;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class DataTemplatePropertyHandler<TElementType, TItemType> : IMauiContainerElementHandler, INonChildContainerElement
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

        public void Remove()
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex) { }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child) { }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child) => -1;

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
