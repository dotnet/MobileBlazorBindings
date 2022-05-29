// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class RadioButton : TemplatedView
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public object Value { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (Title != null || Value != null)
            {
                builder.AddAttribute(nameof(Title), Title ?? Value.ToString());
            }
            if (Value != null)
            {
                builder.AddAttribute(nameof(Value), AttributeHelper.ObjectToDelegate(Value));
            }
        }
    }
}
