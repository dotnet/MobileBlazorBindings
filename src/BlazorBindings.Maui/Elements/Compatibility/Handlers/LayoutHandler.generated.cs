// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MCC = Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Compatibility.Handlers
{
    public abstract partial class LayoutHandler : BlazorBindings.Maui.Elements.Handlers.ViewHandler
    {
        private static readonly bool CascadeInputTransparentDefaultValue = MCC.Layout.CascadeInputTransparentProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsClippedToBoundsDefaultValue = MCC.Layout.IsClippedToBoundsProperty.DefaultValue is bool value ? value : default;
        private static readonly Thickness PaddingDefaultValue = MCC.Layout.PaddingProperty.DefaultValue is Thickness value ? value : default;

        public LayoutHandler(NativeComponentRenderer renderer, MCC.Layout layoutControl) : base(renderer, layoutControl)
        {
            LayoutControl = layoutControl ?? throw new ArgumentNullException(nameof(layoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCC.Layout LayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MCC.Layout.CascadeInputTransparent):
                    LayoutControl.CascadeInputTransparent = AttributeHelper.GetBool(attributeValue, CascadeInputTransparentDefaultValue);
                    break;
                case nameof(MCC.Layout.IsClippedToBounds):
                    LayoutControl.IsClippedToBounds = AttributeHelper.GetBool(attributeValue, IsClippedToBoundsDefaultValue);
                    break;
                case nameof(MCC.Layout.Padding):
                    LayoutControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
