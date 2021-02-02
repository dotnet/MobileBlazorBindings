// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class NavigableElementHandler : ElementHandler
    {

        public NavigableElementHandler(NativeComponentRenderer renderer, XF.NavigableElement navigableElementControl) : base(renderer, navigableElementControl)
        {
            NavigableElementControl = navigableElementControl ?? throw new ArgumentNullException(nameof(navigableElementControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.NavigableElement NavigableElementControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.NavigableElement.@class):
                    NavigableElementControl.@class = AttributeHelper.GetStringList(attributeValue);
                    break;
                case nameof(XF.NavigableElement.StyleClass):
                    NavigableElementControl.StyleClass = AttributeHelper.GetStringList(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
