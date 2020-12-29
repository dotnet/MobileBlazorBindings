// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    internal class MobileBlazorBindingsElementManager : ElementManager<IXamarinFormsElementHandler>
    {
        protected override bool IsParented(IXamarinFormsElementHandler handler)
        {
            return handler.IsParented();
        }

        protected override void AddChildElement(
            IXamarinFormsElementHandler parentHandler,
            IXamarinFormsElementHandler childHandler,
            int physicalSiblingIndex)
        {
            if (childHandler is INonPhysicalChild nonPhysicalChild)
            {
                // If the child is a non-child container then we shouldn't try to add it to a parent.
                // This is used in cases such as ModalContainer, which exists for the purposes of Blazor
                // markup and is not represented in the Xamarin.Forms control hierarchy.

                nonPhysicalChild.SetParent(parentHandler.ElementControl);
                return;
            }

            if (parentHandler.ElementControl is Application parentAsApp)
            {
                if (childHandler.ElementControl is Page childControlAsPage)
                {
                    //MainPage may already be set, but it is safe to replace it.
                    parentAsApp.MainPage = childControlAsPage;
                }
                else
                {
                    throw new InvalidOperationException($"Application MainPage must be a Page; cannot set {parentAsApp.GetType().FullName}'s MainPage to {childHandler.ElementControl.GetType().FullName}");
                }
                return;
            }

            if (!(parentHandler is IXamarinFormsContainerElementHandler parent))
            {
                throw new NotSupportedException($"Handler of type '{parentHandler.GetType().FullName}' representing element type " +
                    $"'{parentHandler.ElementControl?.GetType().FullName ?? "<null>"}' doesn't support adding a child " +
                    $"(child type is '{childHandler.ElementControl?.GetType().FullName}').");
            }

            parent.AddChild(childHandler.ElementControl, physicalSiblingIndex);

            if (!(parentHandler is INonChildContainerElement))
            {
                // Notify the child handler that its parent was set. This is needed for cases
                // where the parent/child are a conceptual relationship and not represented
                // by the Xamarin.Forms control hierarchy.
                childHandler.SetParent(parentHandler.ElementControl);
            }
        }

        protected override int GetChildElementIndex(IXamarinFormsElementHandler parentHandler, IXamarinFormsElementHandler childHandler)
        {
            return parentHandler is IXamarinFormsContainerElementHandler containerHandler
                ? containerHandler.GetChildIndex(childHandler.ElementControl)
                : -1;
        }

        protected override void RemoveChildElement(IXamarinFormsElementHandler parentHandler, IXamarinFormsElementHandler childHandler)
        {
            if (parentHandler is IXamarinFormsContainerElementHandler parent)
            {
                parent.RemoveChild(childHandler.ElementControl);
            }
            else
            {
                throw new NotSupportedException($"Handler of type '{parentHandler.GetType().FullName}' representing element type " +
                    $"'{parentHandler.ElementControl?.GetType().FullName ?? "<null>"}' doesn't support removing a child " +
                    $"(child type is '{childHandler.ElementControl?.GetType().FullName}').");
            }
        }

        protected override bool IsParentOfChild(IXamarinFormsElementHandler parentHandler, IXamarinFormsElementHandler childHandler)
        {
            return childHandler.IsParentedTo(parentHandler.ElementControl);
        }
    }
}
