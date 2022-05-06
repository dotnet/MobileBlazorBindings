// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class StackLayoutHandler : StackBaseHandler
    {
        private static readonly MC.StackOrientation OrientationDefaultValue = MC.StackLayout.OrientationProperty.DefaultValue is MC.StackOrientation value ? value : default;

        public StackLayoutHandler(NativeComponentRenderer renderer, MC.StackLayout stackLayoutControl) : base(renderer, stackLayoutControl)
        {
            StackLayoutControl = stackLayoutControl ?? throw new ArgumentNullException(nameof(stackLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.StackLayout StackLayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.StackLayout.Orientation):
                    StackLayoutControl.Orientation = (MC.StackOrientation)AttributeHelper.GetInt(attributeValue, (int)OrientationDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
