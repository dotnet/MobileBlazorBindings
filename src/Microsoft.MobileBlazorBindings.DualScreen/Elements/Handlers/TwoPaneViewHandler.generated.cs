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
                    TwoPaneViewControl.MinTallModeHeight = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XFD.TwoPaneView.MinWideModeWidth):
                    TwoPaneViewControl.MinWideModeWidth = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XFD.TwoPaneView.Pane1Length):
                    TwoPaneViewControl.Pane1Length = AttributeHelper.StringToGridLength(attributeValue, XF.GridLength.Star);
                    break;
                case nameof(XFD.TwoPaneView.Pane2Length):
                    TwoPaneViewControl.Pane2Length = AttributeHelper.StringToGridLength(attributeValue, XF.GridLength.Star);
                    break;
                case nameof(XFD.TwoPaneView.PanePriority):
                    TwoPaneViewControl.PanePriority = (XFD.TwoPaneViewPriority)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XFD.TwoPaneView.TallModeConfiguration):
                    TwoPaneViewControl.TallModeConfiguration = (XFD.TwoPaneViewTallModeConfiguration)AttributeHelper.GetInt(attributeValue, (int)XFD.TwoPaneViewTallModeConfiguration.TopBottom);
                    break;
                case nameof(XFD.TwoPaneView.WideModeConfiguration):
                    TwoPaneViewControl.WideModeConfiguration = (XFD.TwoPaneViewWideModeConfiguration)AttributeHelper.GetInt(attributeValue, (int)XFD.TwoPaneViewWideModeConfiguration.LeftRight);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
