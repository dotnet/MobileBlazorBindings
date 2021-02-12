// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    /// <summary>
    /// Fake element handler, which is used as a root for a renderer to get native Xamarin.Forms elements
    /// from a Blazor component.
    /// </summary>
    public class RootContainerHandler : IXamarinFormsContainerElementHandler
    {
        public List<XF.Element> Elements { get; } = new List<XF.Element>();

        public Task WaitForElementAsync()
        {
            if (Elements.Count > 0)
            {
                return Task.CompletedTask;
            }

            var taskCompletionSource = new TaskCompletionSource<XF.Element>();
            ChildAdded += SetTaskResult;
            return taskCompletionSource.Task;

            void SetTaskResult(XF.Element element)
            {
                ChildAdded -= SetTaskResult;
                taskCompletionSource.TrySetResult(element);
            }
        }

        private event Action<XF.Element> ChildAdded;

        void IXamarinFormsContainerElementHandler.AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var index = Math.Min(physicalSiblingIndex, Elements.Count);
            Elements.Insert(index, child);
            ChildAdded?.Invoke(child);
        }

        void IXamarinFormsContainerElementHandler.RemoveChild(XF.Element child)
        {
            Elements.Remove(child);
        }

        int IXamarinFormsContainerElementHandler.GetChildIndex(XF.Element child)
        {
            return Elements.IndexOf(child);
        }

        bool IXamarinFormsElementHandler.IsParentedTo(XF.Element parent)
        {
            return Elements.Contains(parent);
        }

        // Because this is a 'fake' container element, all matters related to physical trees
        // should be no-ops.
        XF.Element IXamarinFormsElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
        bool IXamarinFormsElementHandler.IsParented() => false;
        void IXamarinFormsElementHandler.SetParent(XF.Element parent) { }
    }
}
