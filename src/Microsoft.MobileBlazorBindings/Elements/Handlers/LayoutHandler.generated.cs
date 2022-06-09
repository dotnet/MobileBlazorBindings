// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class LayoutHandler : ViewHandler
    {
        private static readonly bool CascadeInputTransparentDefaultValue = MC.Layout.CascadeInputTransparentProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsClippedToBoundsDefaultValue = MC.Layout.IsClippedToBoundsProperty.DefaultValue is bool value ? value : default;
        private static readonly Thickness PaddingDefaultValue = MC.Layout.PaddingProperty.DefaultValue is Thickness value ? value : default;

        public LayoutHandler(NativeComponentRenderer renderer, MC.Layout layoutControl) : base(renderer, layoutControl)
        {
            LayoutControl = layoutControl ?? throw new ArgumentNullException(nameof(layoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Layout LayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Layout.CascadeInputTransparent):
                    LayoutControl.CascadeInputTransparent = AttributeHelper.GetBool(attributeValue, CascadeInputTransparentDefaultValue);
                    break;
                case nameof(MC.Layout.IgnoreSafeArea):
                    LayoutControl.IgnoreSafeArea = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(MC.Layout.IsClippedToBounds):
                    LayoutControl.IsClippedToBounds = AttributeHelper.GetBool(attributeValue, IsClippedToBoundsDefaultValue);
                    break;
                case nameof(MC.Layout.Padding):
                    LayoutControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
