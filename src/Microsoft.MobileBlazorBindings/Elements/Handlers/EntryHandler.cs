// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class EntryHandler : InputViewHandler
    {
        public EntryHandler(NativeComponentRenderer renderer, XF.Entry entryControl) : base(renderer, entryControl)
        {
            EntryControl = entryControl ?? throw new ArgumentNullException(nameof(entryControl));
            EntryControl.Completed += (s, e) =>
            {
                if (CompletedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(CompletedEventHandlerId, null, e));
                }
            };
            EntryControl.TextChanged += (s, e) =>
            {
                if (TextChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = EntryControl.Text }));
                }
            };
        }

        public ulong CompletedEventHandlerId { get; set; }
        public ulong TextChangedEventHandlerId { get; set; }
        public XF.Entry EntryControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Entry.CharacterSpacing):
                    EntryControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Entry.ClearButtonVisibility):
                    EntryControl.ClearButtonVisibility = (XF.ClearButtonVisibility)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Entry.CursorPosition):
                    EntryControl.CursorPosition = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Entry.FontAttributes):
                    EntryControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Entry.FontFamily):
                    EntryControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(XF.Entry.FontSize):
                    EntryControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Entry.HorizontalTextAlignment):
                    EntryControl.HorizontalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Entry.IsPassword):
                    EntryControl.IsPassword = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Entry.IsTextPredictionEnabled):
                    EntryControl.IsTextPredictionEnabled = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Entry.Placeholder):
                    EntryControl.Placeholder = (string)attributeValue;
                    break;
                case nameof(XF.Entry.PlaceholderColor):
                    EntryControl.PlaceholderColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Entry.ReturnType):
                    EntryControl.ReturnType = (XF.ReturnType)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Entry.SelectionLength):
                    EntryControl.SelectionLength = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Entry.Text):
                    EntryControl.Text = (string)attributeValue;
                    break;
                case nameof(XF.Entry.TextColor):
                    EntryControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Entry.VerticalTextAlignment):
                    EntryControl.VerticalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;

                case "oncompleted":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => CompletedEventHandlerId = 0);
                    CompletedEventHandlerId = attributeEventHandlerId;
                    break;
                case "ontextchanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => TextChangedEventHandlerId = 0);
                    TextChangedEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
