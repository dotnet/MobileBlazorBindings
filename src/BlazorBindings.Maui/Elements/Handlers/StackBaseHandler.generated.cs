// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract partial class StackBaseHandler : LayoutHandler
    {
        private static readonly double SpacingDefaultValue = MC.StackBase.SpacingProperty.DefaultValue is double value ? value : default;

        public StackBaseHandler(NativeComponentRenderer renderer, MC.StackBase stackBaseControl) : base(renderer, stackBaseControl)
        {
            StackBaseControl = stackBaseControl ?? throw new ArgumentNullException(nameof(stackBaseControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.StackBase StackBaseControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.StackBase.Spacing):
                    StackBaseControl.Spacing = AttributeHelper.StringToDouble((string)attributeValue, SpacingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
