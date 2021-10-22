// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Editor : InputView
    {
        static Editor()
        {
            ElementHandlerRegistry.RegisterElementHandler<Editor>(
                renderer => new EditorHandler(renderer, new XF.Editor()));
        }

        /// <summary>
        /// Gets or sets a value that controls whether the editor will change size to accommodate input as the user enters it.
        /// </summary>
        /// <value>
        /// Whether the editor will change size to accommodate input as the user enters it.
        /// </value>
        [Parameter] public XF.EditorAutoSizeOption? AutoSize { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the editor is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family to which the font for the editor belongs.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the editor.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public bool? IsTextPredictionEnabled { get; set; }

        public new XF.Editor NativeControl => ((EditorHandler)ElementHandler).EditorControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutoSize != null)
            {
                builder.AddAttribute(nameof(AutoSize), (int)AutoSize.Value);
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
            if (IsTextPredictionEnabled != null)
            {
                builder.AddAttribute(nameof(IsTextPredictionEnabled), IsTextPredictionEnabled.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
