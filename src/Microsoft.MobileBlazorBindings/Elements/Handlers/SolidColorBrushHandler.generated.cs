// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SolidColorBrushHandler : BrushHandler
    {
        private static readonly XF.Color ColorDefaultValue = XF.SolidColorBrush.ColorProperty.DefaultValue is XF.Color value ? value : default;

        public SolidColorBrushHandler(NativeComponentRenderer renderer, XF.SolidColorBrush solidColorBrushControl) : base(renderer, solidColorBrushControl)
        {
            SolidColorBrushControl = solidColorBrushControl ?? throw new ArgumentNullException(nameof(solidColorBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.SolidColorBrush SolidColorBrushControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.SolidColorBrush.Color):
                    SolidColorBrushControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
