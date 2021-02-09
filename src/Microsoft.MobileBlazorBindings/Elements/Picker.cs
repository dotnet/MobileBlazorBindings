// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Picker<TItem> : View
    {
        public new XF.Picker NativeControl => base.NativeControl as XF.Picker;

        //Changing source at run time is valid behaviour so do not make readonly
#pragma warning disable CA2227 // Collection properties should be read only
        [Parameter] public IList<TItem> ItemsSource { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
        [Parameter] public string Title { get; set; }
        [Parameter] public string ItemDisplayBinding { get; set; }
        [Parameter] public TItem SelectedItem { get; set; }
        [Parameter] public int SelectedIndex { get; set; } = -1;
        [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
        [Parameter] public EventCallback<int> SelectedIndexChanged { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the label is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family to which the font for the label belongs.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the label.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Gets or sets the horizontal alignment of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }

#pragma warning disable CA1200 // Avoid using cref tags with a prefix; these are copied from Xamarin.Forms as-is
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> for the text of this Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Color" /> value.
        /// </value>
        [Parameter] public XF.Color? TextColor { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> for the tite of this Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Color" /> value.
        /// </value>
        [Parameter] public XF.Color? TitleColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }
        /// <summary>
        /// Gets or sets the vertical alignement of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }
#pragma warning restore CA1200 // Avoid using cref tags with a prefix

        static Picker()
        {
            ElementHandlerRegistry.RegisterElementHandler<Picker<TItem>>(renderer => new PickerHandler(renderer, new XF.Picker()));
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
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
            if (ItemDisplayBinding != null)
            {
                builder.AddAttribute(nameof(ItemDisplayBinding), ItemDisplayBinding);
            }
            if (ItemsSource != null)
            {
                builder.AddAttribute(nameof(ItemsSource), AttributeHelper.ObjectToDelegate(ItemsSource));
            }
            if (SelectedItem != null)
            {
                builder.AddAttribute(nameof(SelectedItem), AttributeHelper.ObjectToDelegate(SelectedItem));
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor.Value));
            }
            if (TextTransform != null)
            {
                builder.AddAttribute(nameof(TextTransform), (int)TextTransform.Value);
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }
            if (TitleColor != null)
            {
                builder.AddAttribute(nameof(TitleColor), AttributeHelper.ColorToString(TitleColor.Value));
            }
            if (VerticalTextAlignment != null)
            {
                builder.AddAttribute(nameof(VerticalTextAlignment), (int)VerticalTextAlignment.Value);
            }

            builder.AddAttribute(nameof(SelectedIndex), SelectedIndex);

            builder.AddAttribute("onselecteditemchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleSelectedItemChanged));

            builder.AddAttribute("onselectedindexchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleSelectedIndexChanged));
        }

        private Task HandleSelectedItemChanged(ChangeEventArgs evt)
        {
            return SelectedItemChanged.InvokeAsync((TItem)evt.Value);
        }

        private Task HandleSelectedIndexChanged(ChangeEventArgs evt)
        {
            return SelectedIndexChanged.InvokeAsync((int)evt.Value);
        }
    }
}
