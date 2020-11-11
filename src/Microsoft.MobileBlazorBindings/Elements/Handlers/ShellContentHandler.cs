// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellContentHandler : BaseShellItemHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsTemplatedPage = child as XF.TemplatedPage;
            ShellContentControl.Content = childAsTemplatedPage;
        }

        public virtual void RemoveChild(XF.Element child)
        {
            if (ShellContentControl.Content == child)
            {
                ShellContentControl.Content = null;
            }
        }

        public int GetChildIndex(XF.Element child)
        {
            return child == ShellContentControl.Content ? 0 : -1;
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
