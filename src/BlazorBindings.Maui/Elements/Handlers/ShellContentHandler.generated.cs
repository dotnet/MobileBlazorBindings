// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellContentHandler : BaseShellItemHandler
    {

        public ShellContentHandler(NativeComponentRenderer renderer, MC.ShellContent shellContentControl) : base(renderer, shellContentControl)
        {
            ShellContentControl = shellContentControl ?? throw new ArgumentNullException(nameof(shellContentControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ShellContent ShellContentControl { get; }
    }
}
