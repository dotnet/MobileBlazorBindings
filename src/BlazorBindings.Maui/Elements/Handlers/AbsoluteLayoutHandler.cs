// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class AbsoluteLayoutHandler : LayoutHandler
    {
        public AbsoluteLayoutHandler(NativeComponentRenderer renderer, MC.AbsoluteLayout layoutControl) : base(renderer, layoutControl)
        {
        }
    }
}
