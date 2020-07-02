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
            if (childHandler is INonChildContainerElement childNonContainer)
            {
                // If the child is a non-child container then we shouldn't try to add it to a parent.
                // This is used in cases such as ModalContainer, which exists for the purposes of Blazor
                // markup and is not represented in the Xamarin.Forms control hierarchy.

                childNonContainer.SetParent(parentHandler.ElementControl);
                return;
            }

            if (parentHandler.ElementControl is Application parentAsApp)
            {
                if (parentAsApp.MainPage != null)
                {
                    throw new InvalidOperationException($"Application already has MainPage set; cannot set {parentAsApp.GetType().FullName}'s MainPage to {childHandler.ElementControl.GetType().FullName}");
                }
                else
                {
                    if (childHandler.ElementControl is Page childControlAsPage)
                    {
                        parentAsApp.MainPage = childControlAsPage;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Application MainPage must be a Page; cannot set {parentAsApp.GetType().FullName}'s MainPage to {childHandler.ElementControl.GetType().FullName}");
                    }
                }
                return;
            }

            parentHandler.AddChild(childHandler.ElementControl, physicalSiblingIndex);

            if (!(parentHandler is INonChildContainerElement))
            {
                // Notify the child handler that its parent was set. This is needed for cases
                // where the parent/child are a conceptual relationship and not represented
                // by the Xamarin.Forms control hierarchy.
                childHandler.SetParent(parentHandler.ElementControl);
            }
        }

        protected override int GetPhysicalSiblingIndex(
            IXamarinFormsElementHandler handler)
        {
            return handler.GetPhysicalSiblingIndex();
        }

        protected override void RemoveElement(IXamarinFormsElementHandler handler)
        {
            // TODO: Need to make this logic more generic; not all parents are Layouts, not all children are Views

            var control = handler.ElementControl;
            var physicalParent = control.Parent;
            if (physicalParent is Layout<View> physicalParentAsLayout)
            {
                var childTargetAsView = control as View;
                physicalParentAsLayout.Children.Remove(childTargetAsView);
            }
        }

        protected override bool IsParentOfChild(IXamarinFormsElementHandler parentHandler, IXamarinFormsElementHandler childHandler)
        {
            return childHandler.IsParentedTo(parentHandler.ElementControl);
        }
    }
}
