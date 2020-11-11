// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellItemHandler : ShellGroupItemHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            XF.ShellSection sectionToAdd = child switch
            {
                XF.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                XF.ShellContent childAsShellContent => childAsShellContent,  // Implicit conversion
                XF.ShellSection childAsShellSection => childAsShellSection,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{child.GetType().FullName}').")
            };

            if (ShellItemControl.Items.Count >= physicalSiblingIndex)
            {
                ShellItemControl.Items.Insert(physicalSiblingIndex, sectionToAdd);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but ShellItemControl.Items.Count={ShellItemControl.Items.Count}");
                ShellItemControl.Items.Add(sectionToAdd);
            }
        }

        public virtual void RemoveChild(XF.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            var sectionToRemove = GetSectionForElement(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            ShellItemControl.Items.Remove(sectionToRemove);
        }

        public override void SetParent(XF.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }

        public int GetChildIndex(XF.Element child)
        {
            var section = GetSectionForElement(child);
            return ShellItemControl.Items.IndexOf(section);
        }

        private XF.ShellSection GetSectionForElement(XF.Element child)
        {
            return child switch
            {
                XF.TemplatedPage childAsTemplatedPage => GetSectionForTemplatedPage(childAsTemplatedPage),
                XF.ShellContent childAsShellContent => GetSectionForContent(childAsShellContent),
                XF.ShellSection childAsShellSection => childAsShellSection,
                _ => null
            };
        }

        private XF.ShellSection GetSectionForContent(XF.ShellContent shellContent)
        {
            return ShellItemControl.Items.FirstOrDefault(section => section.Items.Contains(shellContent));
        }

        private XF.ShellSection GetSectionForTemplatedPage(XF.TemplatedPage templatedPage)
        {
            return ShellItemControl.Items
                .FirstOrDefault(section => section.Items.Any(contect => contect.Content == templatedPage));
        }
    }
}
