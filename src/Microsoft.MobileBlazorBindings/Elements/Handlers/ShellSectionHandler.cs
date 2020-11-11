// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellSectionHandler : ShellGroupItemHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            XF.ShellContent contentToAdd = child switch
            {
                XF.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                XF.ShellContent childAsShellContent => childAsShellContent,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{child.GetType().FullName}').")
            };

            // Ensure that there is non-null Content to avoid exceptions in Xamarin.Forms
            contentToAdd.Content ??= new XF.Page();

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

        public int GetChildIndex(XF.Element child)
        {
            var shellContent = GetContentForChild(child);
            return ShellSectionControl.Items.IndexOf(shellContent);
        }

        public virtual void RemoveChild(XF.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            XF.ShellContent contentToRemove = GetContentForChild(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            ShellSectionControl.Items.Remove(contentToRemove);
        }

        public override void SetParent(XF.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }

        private XF.ShellContent GetContentForChild(XF.Element child)
        {
            return child switch
            {
                XF.TemplatedPage childAsTemplatedPage => GetContentForTemplatePage(childAsTemplatedPage),
                XF.ShellContent childAsShellContent => childAsShellContent,
                _ => null
            };
        }

        private XF.ShellContent GetContentForTemplatePage(XF.TemplatedPage childAsTemplatedPage)
        {
            return ShellSectionControl.Items.FirstOrDefault(content => content.Content == childAsTemplatedPage);
        }
    }
}
