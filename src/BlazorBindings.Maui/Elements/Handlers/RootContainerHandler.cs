// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <summary>
    /// Fake element handler, which is used as a root for a renderer to get native Xamarin.Forms elements
    /// from a Blazor component.
    /// </summary>
    public class RootContainerHandler : IMauiContainerElementHandler
    {
        public List<MC.Element> Elements { get; } = new List<MC.Element>();

        public Task WaitForElementAsync()
        {
            if (Elements.Count > 0)
            {
                return Task.CompletedTask;
            }

            var taskCompletionSource = new TaskCompletionSource<MC.Element>();
            ChildAdded += SetTaskResult;
            return taskCompletionSource.Task;

            void SetTaskResult(MC.Element element)
            {
                ChildAdded -= SetTaskResult;
                taskCompletionSource.TrySetResult(element);
            }
        }

        private event Action<MC.Element> ChildAdded;

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var index = Math.Min(physicalSiblingIndex, Elements.Count);
            Elements.Insert(index, child);
            ChildAdded?.Invoke(child);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            Elements.Remove(child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            return Elements.IndexOf(child);
        }

        bool IMauiElementHandler.IsParentedTo(MC.Element parent)
        {
            return Elements.Contains(parent);
        }

        // Because this is a 'fake' container element, all matters related to physical trees
        // should be no-ops.
        MC.Element IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
        bool IMauiElementHandler.IsParented() => false;
        void IMauiElementHandler.SetParent(MC.Element parent) { }
    }
}
