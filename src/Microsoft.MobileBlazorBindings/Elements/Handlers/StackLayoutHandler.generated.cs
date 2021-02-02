// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class StackLayoutHandler : LayoutHandler
    {
        private static readonly XF.StackOrientation OrientationDefaultValue = XF.StackLayout.OrientationProperty.DefaultValue is XF.StackOrientation value ? value : default;
        private static readonly double SpacingDefaultValue = XF.StackLayout.SpacingProperty.DefaultValue is double value ? value : default;

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
                    StackLayoutControl.Orientation = (XF.StackOrientation)AttributeHelper.GetInt(attributeValue, (int)OrientationDefaultValue);
                    break;
                case nameof(XF.StackLayout.Spacing):
                    StackLayoutControl.Spacing = AttributeHelper.StringToDouble((string)attributeValue, SpacingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
