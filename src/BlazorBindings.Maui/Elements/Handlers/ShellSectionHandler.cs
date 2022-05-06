// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellSectionHandler : ShellGroupItemHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            MC.ShellContent contentToAdd = child switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{child.GetType().FullName}').")
            };

            // Ensure that there is non-null Content to avoid exceptions in Xamarin.Forms
            contentToAdd.Content ??= new MC.Page();

            if (ShellSectionControl.Items.Count >= physicalSiblingIndex)
            {
                ShellSectionControl.Items.Insert(physicalSiblingIndex, contentToAdd);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but ShellSectionControl.Items.Count={ShellSectionControl.Items.Count}");
                ShellSectionControl.Items.Add(contentToAdd);
            }
        }

        public int GetChildIndex(MC.Element child)
        {
            var shellContent = GetContentForChild(child);
            return ShellSectionControl.Items.IndexOf(shellContent);
        }

        public virtual void RemoveChild(MC.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            MC.ShellContent contentToRemove = GetContentForChild(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            ShellSectionControl.Items.Remove(contentToRemove);
        }

        public override void SetParent(MC.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }

        private MC.ShellContent GetContentForChild(MC.Element child)
        {
            return child switch
            {
                MC.TemplatedPage childAsTemplatedPage => GetContentForTemplatePage(childAsTemplatedPage),
                MC.ShellContent childAsShellContent => childAsShellContent,
                _ => null
            };
        }

        private MC.ShellContent GetContentForTemplatePage(MC.TemplatedPage childAsTemplatedPage)
        {
            return ShellSectionControl.Items.FirstOrDefault(content => content.Content == childAsTemplatedPage);
        }
    }
}
