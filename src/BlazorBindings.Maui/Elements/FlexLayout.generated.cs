// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Layouts;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlexLayout : Layout
    {
        static FlexLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<FlexLayout>(
                renderer => new FlexLayoutHandler(renderer, new MC.FlexLayout()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public FlexAlignContent? AlignContent { get; set; }
        [Parameter] public FlexAlignItems? AlignItems { get; set; }
        [Parameter] public FlexDirection? Direction { get; set; }
        [Parameter] public FlexJustify? JustifyContent { get; set; }
        [Parameter] public FlexPosition? Position { get; set; }
        [Parameter] public FlexWrap? Wrap { get; set; }

        public new MC.FlexLayout NativeControl => (ElementHandler as FlexLayoutHandler)?.FlexLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AlignContent != null)
            {
                builder.AddAttribute(nameof(AlignContent), (int)AlignContent.Value);
            }
            if (AlignItems != null)
            {
                builder.AddAttribute(nameof(AlignItems), (int)AlignItems.Value);
            }
            if (Direction != null)
            {
                builder.AddAttribute(nameof(Direction), (int)Direction.Value);
            }
            if (JustifyContent != null)
            {
                builder.AddAttribute(nameof(JustifyContent), (int)JustifyContent.Value);
            }
            if (Position != null)
            {
                builder.AddAttribute(nameof(Position), (int)Position.Value);
            }
            if (Wrap != null)
            {
                builder.AddAttribute(nameof(Wrap), (int)Wrap.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
