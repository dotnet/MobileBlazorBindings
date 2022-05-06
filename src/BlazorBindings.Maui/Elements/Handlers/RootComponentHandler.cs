// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using WVM = Microsoft.AspNetCore.Components.WebView.Maui;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class RootComponentHandler : IMauiElementHandler, INonPhysicalChild
    {
        private WVM.BlazorWebView _parentWebView;

        object IElementHandler.TargetElement => RootComponentControl;

        public WVM.RootComponent RootComponentControl { get; }

        public RootComponentHandler(WVM.RootComponent rootComponentControl)
        {
            RootComponentControl = rootComponentControl;
        }

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(WVM.RootComponent.ComponentType):
                    RootComponentControl.ComponentType = AttributeHelper.DelegateToObject<Type>(attributeValue);
                    break;
                case nameof(WVM.RootComponent.Parameters):
                    RootComponentControl.Parameters = AttributeHelper.DelegateToObject<IDictionary<string, object>>(attributeValue);
                    break;
                case nameof(WVM.RootComponent.Selector):
                    RootComponentControl.Selector = (string)attributeValue;
                    break;
            }
        }

        public void Remove()
        {
            _parentWebView.RootComponents.Remove(RootComponentControl);
        }

        public void SetParent(object parentElement)
        {
            _parentWebView = (WVM.BlazorWebView)parentElement;
            _parentWebView.RootComponents.Add(RootComponentControl);
        }

        Microsoft.Maui.Controls.Element IMauiElementHandler.ElementControl => null;

        bool IMauiElementHandler.IsParented() => _parentWebView is not null;

        bool IMauiElementHandler.IsParentedTo(Microsoft.Maui.Controls.Element parent) => parent.Equals(_parentWebView);

        void IMauiElementHandler.SetParent(Microsoft.Maui.Controls.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }
    }
}