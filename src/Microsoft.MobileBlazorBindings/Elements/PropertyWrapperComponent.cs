// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements
{
    /// <summary>
    /// The only purpose of this type is to wrap content property handler, since currently renderer does not allow 
    /// handlers without corresponding component.
    /// </summary>
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Class is used generically.
    internal class PropertyWrapperComponent : NativeControlComponentBase
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ChildContent(builder);
        }
    }
}
