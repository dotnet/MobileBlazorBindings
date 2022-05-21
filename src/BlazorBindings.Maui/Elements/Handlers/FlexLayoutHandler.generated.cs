// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class FlexLayoutHandler : LayoutHandler
    {
        private static readonly FlexAlignContent AlignContentDefaultValue = MC.FlexLayout.AlignContentProperty.DefaultValue is FlexAlignContent value ? value : default;
        private static readonly FlexAlignItems AlignItemsDefaultValue = MC.FlexLayout.AlignItemsProperty.DefaultValue is FlexAlignItems value ? value : default;
        private static readonly FlexDirection DirectionDefaultValue = MC.FlexLayout.DirectionProperty.DefaultValue is FlexDirection value ? value : default;
        private static readonly FlexJustify JustifyContentDefaultValue = MC.FlexLayout.JustifyContentProperty.DefaultValue is FlexJustify value ? value : default;
        private static readonly FlexPosition PositionDefaultValue = MC.FlexLayout.PositionProperty.DefaultValue is FlexPosition value ? value : default;
        private static readonly FlexWrap WrapDefaultValue = MC.FlexLayout.WrapProperty.DefaultValue is FlexWrap value ? value : default;

        public FlexLayoutHandler(NativeComponentRenderer renderer, MC.FlexLayout flexLayoutControl) : base(renderer, flexLayoutControl)
        {
            FlexLayoutControl = flexLayoutControl ?? throw new ArgumentNullException(nameof(flexLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.FlexLayout FlexLayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.FlexLayout.AlignContent):
                    FlexLayoutControl.AlignContent = (FlexAlignContent)AttributeHelper.GetInt(attributeValue, (int)AlignContentDefaultValue);
                    break;
                case nameof(MC.FlexLayout.AlignItems):
                    FlexLayoutControl.AlignItems = (FlexAlignItems)AttributeHelper.GetInt(attributeValue, (int)AlignItemsDefaultValue);
                    break;
                case nameof(MC.FlexLayout.Direction):
                    FlexLayoutControl.Direction = (FlexDirection)AttributeHelper.GetInt(attributeValue, (int)DirectionDefaultValue);
                    break;
                case nameof(MC.FlexLayout.JustifyContent):
                    FlexLayoutControl.JustifyContent = (FlexJustify)AttributeHelper.GetInt(attributeValue, (int)JustifyContentDefaultValue);
                    break;
                case nameof(MC.FlexLayout.Position):
                    FlexLayoutControl.Position = (FlexPosition)AttributeHelper.GetInt(attributeValue, (int)PositionDefaultValue);
                    break;
                case nameof(MC.FlexLayout.Wrap):
                    FlexLayoutControl.Wrap = (FlexWrap)AttributeHelper.GetInt(attributeValue, (int)WrapDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
