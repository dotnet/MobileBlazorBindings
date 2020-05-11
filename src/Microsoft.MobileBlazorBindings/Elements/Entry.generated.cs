// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Entry : InputView
    {
        static Entry()
        {
            ElementHandlerRegistry.RegisterElementHandler<Entry>(
                renderer => new EntryHandler(renderer, new XF.Entry()));
        }

        [Parameter] public XF.ClearButtonVisibility? ClearButtonVisibility { get; set; }
        /// <summary>
        /// Gets or sets the position of the cursor.
        /// </summary>
        /// <value>
        /// The position of the cursor.
        /// </value>
        [Parameter] public int? CursorPosition { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the Entry element text is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family for the Entry element text.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the Entry element text.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Gets or sets the horizontal text alignment.
        /// </summary>
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates if the entry should visually obscure typed text.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the element is a password box; otherwise, <see langword="false" />. Default value is <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsPassword { get; set; }
        /// <summary>
        /// Gets or sets a value that controls whether text prediction and automatic text correction is on or off.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if text correction is on. Otherwise, <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsTextPredictionEnabled { get; set; }
        /// <summary>
        /// Gets or sets an enumeration value that controls the appearance of the return button.
        /// </summary>
        /// <value>
        /// An enumeration value that controls the appearance of the return button.
        /// </value>
        [Parameter] public XF.ReturnType? ReturnType { get; set; }
        /// <summary>
        /// Gets the length of the selection.
        /// </summary>
        /// <value>
        /// The length of the selection.
        /// </value>
        [Parameter] public int? SelectionLength { get; set; }
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }

        public new XF.Entry NativeControl => ((EntryHandler)ElementHandler).EntryControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (ClearButtonVisibility != null)
            {
                builder.AddAttribute(nameof(ClearButtonVisibility), (int)ClearButtonVisibility.Value);
            }
            if (CursorPosition != null)
            {
                builder.AddAttribute(nameof(CursorPosition), CursorPosition.Value);
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (FontFamily != null)
            {
                builder.AddAttribute(nameof(FontFamily), FontFamily);
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), AttributeHelper.DoubleToString(FontSize.Value));
            }
            if (HorizontalTextAlignment != null)
            {
                builder.AddAttribute(nameof(HorizontalTextAlignment), (int)HorizontalTextAlignment.Value);
            }
            if (IsPassword != null)
            {
                builder.AddAttribute(nameof(IsPassword), IsPassword.Value);
            }
            if (IsTextPredictionEnabled != null)
            {
                builder.AddAttribute(nameof(IsTextPredictionEnabled), IsTextPredictionEnabled.Value);
            }
            if (ReturnType != null)
            {
                builder.AddAttribute(nameof(ReturnType), (int)ReturnType.Value);
            }
            if (SelectionLength != null)
            {
                builder.AddAttribute(nameof(SelectionLength), SelectionLength.Value);
            }
            if (VerticalTextAlignment != null)
            {
                builder.AddAttribute(nameof(VerticalTextAlignment), (int)VerticalTextAlignment.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
