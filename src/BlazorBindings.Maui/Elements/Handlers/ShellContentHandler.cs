// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellContentHandler : BaseShellItemHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsTemplatedPage = child as MC.TemplatedPage;
            ShellContentControl.Content = childAsTemplatedPage;
        }

        public virtual void RemoveChild(MC.Element child)
        {
            if (ShellContentControl.Content == child)
            {
                ShellContentControl.Content = null;
            }
        }

        public int GetChildIndex(MC.Element child)
        {
            return child == ShellContentControl.Content ? 0 : -1;
        }

        public override void SetParent(MC.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }
    }
}
