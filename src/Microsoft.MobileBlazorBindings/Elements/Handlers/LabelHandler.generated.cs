// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler
    {
        public LabelHandler(NativeComponentRenderer renderer, XF.Label labelControl) : base(renderer, labelControl)
        {
            LabelControl = labelControl ?? throw new ArgumentNullException(nameof(labelControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Label LabelControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Label.CharacterSpacing):
                    LabelControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Label.FontAttributes):
                    LabelControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.FontFamily):
                    LabelControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(XF.Label.FontSize):
                    LabelControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.Label.HorizontalTextAlignment):
                    LabelControl.HorizontalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.LineBreakMode):
                    LabelControl.LineBreakMode = (XF.LineBreakMode)AttributeHelper.GetInt(attributeValue, (int)XF.LineBreakMode.WordWrap);
                    break;
                case nameof(XF.Label.LineHeight):
                    LabelControl.LineHeight = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.Label.MaxLines):
                    LabelControl.MaxLines = AttributeHelper.GetInt(attributeValue, -1);
                    break;
                case nameof(XF.Label.Padding):
                    LabelControl.Padding = AttributeHelper.StringToThickness(attributeValue);
                    break;
                case nameof(XF.Label.Text):
                    LabelControl.Text = (string)attributeValue;
                    break;
                case nameof(XF.Label.TextColor):
                    LabelControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Label.TextDecorations):
                    LabelControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.TextTransform):
                    LabelControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)XF.TextTransform.Default);
                    break;
                case nameof(XF.Label.TextType):
                    LabelControl.TextType = (XF.TextType)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.VerticalTextAlignment):
                    LabelControl.VerticalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
