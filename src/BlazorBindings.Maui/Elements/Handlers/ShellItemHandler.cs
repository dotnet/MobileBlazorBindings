// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellItemHandler : ShellGroupItemHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            MC.ShellSection sectionToAdd = child switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent,  // Implicit conversion
                MC.ShellSection childAsShellSection => childAsShellSection,
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

        public virtual void RemoveChild(MC.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            var sectionToRemove = GetSectionForElement(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            ShellItemControl.Items.Remove(sectionToRemove);
        }

        public override void SetParent(MC.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }

        public int GetChildIndex(MC.Element child)
        {
            var section = GetSectionForElement(child);
            return ShellItemControl.Items.IndexOf(section);
        }

        private MC.ShellSection GetSectionForElement(MC.Element child)
        {
            return child switch
            {
                MC.TemplatedPage childAsTemplatedPage => GetSectionForTemplatedPage(childAsTemplatedPage),
                MC.ShellContent childAsShellContent => GetSectionForContent(childAsShellContent),
                MC.ShellSection childAsShellSection => childAsShellSection,
                _ => null
            };
        }

        private MC.ShellSection GetSectionForContent(MC.ShellContent shellContent)
        {
            return ShellItemControl.Items.FirstOrDefault(section => section.Items.Contains(shellContent));
        }

        private MC.ShellSection GetSectionForTemplatedPage(MC.TemplatedPage templatedPage)
        {
            return ShellItemControl.Items
                .FirstOrDefault(section => section.Items.Any(contect => contect.Content == templatedPage));
        }
    }
}
