// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class CheckBox : View
    {
        static CheckBox()
        {
            ElementHandlerRegistry.RegisterElementHandler<CheckBox>(
                renderer => new CheckBoxHandler(renderer, new MC.CheckBox()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public bool? IsChecked { get; set; }

        public new MC.CheckBox NativeControl => ((CheckBoxHandler)ElementHandler).CheckBoxControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color));
            }
            if (IsChecked != null)
            {
                builder.AddAttribute(nameof(IsChecked), IsChecked.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
