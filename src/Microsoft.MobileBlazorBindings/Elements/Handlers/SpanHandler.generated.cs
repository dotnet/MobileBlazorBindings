// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SpanHandler : GestureElementHandler
    {
        public SpanHandler(NativeComponentRenderer renderer, XF.Span spanControl) : base(renderer, spanControl)
        {
            SpanControl = spanControl ?? throw new ArgumentNullException(nameof(spanControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Span SpanControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Span.BackgroundColor):
                    SpanControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Span.CharacterSpacing):
                    SpanControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Span.FontAttributes):
                    SpanControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Span.FontFamily):
                    SpanControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(XF.Span.FontSize):
                    SpanControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.Span.ForegroundColor):
                    SpanControl.ForegroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Span.LineHeight):
                    SpanControl.LineHeight = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.Span.Text):
                    SpanControl.Text = (string)attributeValue ?? "";
                    break;
                case nameof(XF.Span.TextColor):
                    SpanControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Span.TextDecorations):
                    SpanControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Span.TextTransform):
                    SpanControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)XF.TextTransform.Default);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
