// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class LayoutHandler : ViewHandler
    {
        private static readonly bool CascadeInputTransparentDefaultValue = XF.Layout.CascadeInputTransparentProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsClippedToBoundsDefaultValue = XF.Layout.IsClippedToBoundsProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.Thickness PaddingDefaultValue = XF.Layout.PaddingProperty.DefaultValue is XF.Thickness value ? value : default;

        public LayoutHandler(NativeComponentRenderer renderer, XF.Layout layoutControl) : base(renderer, layoutControl)
        {
            LayoutControl = layoutControl ?? throw new ArgumentNullException(nameof(layoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Layout LayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Layout.CascadeInputTransparent):
                    LayoutControl.CascadeInputTransparent = AttributeHelper.GetBool(attributeValue, CascadeInputTransparentDefaultValue);
                    break;
                case nameof(XF.Layout.IsClippedToBounds):
                    LayoutControl.IsClippedToBounds = AttributeHelper.GetBool(attributeValue, IsClippedToBoundsDefaultValue);
                    break;
                case nameof(XF.Layout.Padding):
                    LayoutControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
