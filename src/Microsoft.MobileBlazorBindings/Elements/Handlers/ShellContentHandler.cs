// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellContentHandler : BaseShellItemHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsTemplatedPage = child as XF.TemplatedPage;
            ShellContentControl.Content = childAsTemplatedPage;
        }

        public override void SetParent(XF.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }
    }
}
