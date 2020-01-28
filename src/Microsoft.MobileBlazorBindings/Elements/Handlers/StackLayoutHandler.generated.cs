// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class StackLayoutHandler : LayoutHandler
    {
        public StackLayoutHandler(NativeComponentRenderer renderer, XF.StackLayout stackLayoutControl) : base(renderer, stackLayoutControl)
        {
            StackLayoutControl = stackLayoutControl ?? throw new ArgumentNullException(nameof(stackLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.StackLayout StackLayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.StackLayout.Orientation):
                    StackLayoutControl.Orientation = (XF.StackOrientation)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.StackLayout.Spacing):
                    StackLayoutControl.Spacing = AttributeHelper.StringToDouble((string)attributeValue, 6.00);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
