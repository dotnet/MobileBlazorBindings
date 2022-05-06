// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;
using XFD = Xamarin.Forms.DualScreen;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TwoPaneViewHandler : GridHandler
    {
        private static readonly double MinTallModeHeightDefaultValue = XFD.TwoPaneView.MinTallModeHeightProperty.DefaultValue is double value ? value : default;
        private static readonly double MinWideModeWidthDefaultValue = XFD.TwoPaneView.MinWideModeWidthProperty.DefaultValue is double value ? value : default;
        private static readonly XF.GridLength Pane1LengthDefaultValue = XFD.TwoPaneView.Pane1LengthProperty.DefaultValue is XF.GridLength value ? value : default;
        private static readonly XF.GridLength Pane2LengthDefaultValue = XFD.TwoPaneView.Pane2LengthProperty.DefaultValue is XF.GridLength value ? value : default;
        private static readonly XFD.TwoPaneViewPriority PanePriorityDefaultValue = XFD.TwoPaneView.PanePriorityProperty.DefaultValue is XFD.TwoPaneViewPriority value ? value : default;
        private static readonly XFD.TwoPaneViewTallModeConfiguration TallModeConfigurationDefaultValue = XFD.TwoPaneView.TallModeConfigurationProperty.DefaultValue is XFD.TwoPaneViewTallModeConfiguration value ? value : default;
        private static readonly XFD.TwoPaneViewWideModeConfiguration WideModeConfigurationDefaultValue = XFD.TwoPaneView.WideModeConfigurationProperty.DefaultValue is XFD.TwoPaneViewWideModeConfiguration value ? value : default;

        public TwoPaneViewHandler(NativeComponentRenderer renderer, XFD.TwoPaneView twoPaneViewControl) : base(renderer, twoPaneViewControl)
        {
            TwoPaneViewControl = twoPaneViewControl ?? throw new ArgumentNullException(nameof(twoPaneViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XFD.TwoPaneView TwoPaneViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XFD.TwoPaneView.MinTallModeHeight):
                    TwoPaneViewControl.MinTallModeHeight = AttributeHelper.StringToDouble((string)attributeValue, MinTallModeHeightDefaultValue);
                    break;
                case nameof(XFD.TwoPaneView.MinWideModeWidth):
                    TwoPaneViewControl.MinWideModeWidth = AttributeHelper.StringToDouble((string)attributeValue, MinWideModeWidthDefaultValue);
                    break;
                case nameof(XFD.TwoPaneView.Pane1Length):
                    TwoPaneViewControl.Pane1Length = AttributeHelper.StringToGridLength(attributeValue, Pane1LengthDefaultValue);
                    break;
                case nameof(XFD.TwoPaneView.Pane2Length):
                    TwoPaneViewControl.Pane2Length = AttributeHelper.StringToGridLength(attributeValue, Pane2LengthDefaultValue);
                    break;
                case nameof(XFD.TwoPaneView.PanePriority):
                    TwoPaneViewControl.PanePriority = (XFD.TwoPaneViewPriority)AttributeHelper.GetInt(attributeValue, (int)PanePriorityDefaultValue);
                    break;
                case nameof(XFD.TwoPaneView.TallModeConfiguration):
                    TwoPaneViewControl.TallModeConfiguration = (XFD.TwoPaneViewTallModeConfiguration)AttributeHelper.GetInt(attributeValue, (int)TallModeConfigurationDefaultValue);
                    break;
                case nameof(XFD.TwoPaneView.WideModeConfiguration):
                    TwoPaneViewControl.WideModeConfiguration = (XFD.TwoPaneViewWideModeConfiguration)AttributeHelper.GetInt(attributeValue, (int)WideModeConfigurationDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
