// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellContentHandler : BaseShellItemHandler
    {

        public ShellContentHandler(NativeComponentRenderer renderer, XF.ShellContent shellContentControl) : base(renderer, shellContentControl)
        {
            ShellContentControl = shellContentControl ?? throw new ArgumentNullException(nameof(shellContentControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ShellContent ShellContentControl { get; }
    }
}
