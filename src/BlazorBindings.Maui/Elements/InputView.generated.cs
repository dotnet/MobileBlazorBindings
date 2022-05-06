// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class InputView : View
    {
        static InputView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public bool? IsReadOnly { get; set; }
        [Parameter] public bool? IsSpellCheckEnabled { get; set; }
        [Parameter] public Keyboard Keyboard { get; set; }
        [Parameter] public int? MaxLength { get; set; }
        [Parameter] public string Placeholder { get; set; }
        [Parameter] public Color PlaceholderColor { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextTransform? TextTransform { get; set; }

        public new MC.InputView NativeControl => ((InputViewHandler)ElementHandler).InputViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
            }
            if (IsReadOnly != null)
            {
                builder.AddAttribute(nameof(IsReadOnly), IsReadOnly.Value);
            }
            if (IsSpellCheckEnabled != null)
            {
                builder.AddAttribute(nameof(IsSpellCheckEnabled), IsSpellCheckEnabled.Value);
            }
            if (Keyboard != null)
            {
                builder.AddAttribute(nameof(Keyboard), AttributeHelper.ObjectToDelegate(Keyboard));
            }
            if (MaxLength != null)
            {
                builder.AddAttribute(nameof(MaxLength), MaxLength.Value);
            }
            if (Placeholder != null)
            {
                builder.AddAttribute(nameof(Placeholder), Placeholder);
            }
            if (PlaceholderColor != null)
            {
                builder.AddAttribute(nameof(PlaceholderColor), AttributeHelper.ColorToString(PlaceholderColor));
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor));
            }
            if (TextTransform != null)
            {
                builder.AddAttribute(nameof(TextTransform), (int)TextTransform.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
