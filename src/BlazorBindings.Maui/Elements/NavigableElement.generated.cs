// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class NavigableElement : Element
    {
        static NavigableElement()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public string @class { get; set; }
        [Parameter] public string StyleClass { get; set; }

        public new MC.NavigableElement NativeControl => (ElementHandler as NavigableElementHandler)?.NavigableElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (@class != null)
            {
                builder.AddAttribute(nameof(@class), @class);
            }
            if (StyleClass != null)
            {
                builder.AddAttribute(nameof(StyleClass), StyleClass);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
