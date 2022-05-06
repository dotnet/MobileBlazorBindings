// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract class BaseAttachedPropertiesHandler : IMauiElementHandler, INonPhysicalChild
    {
        /// <summary>
        /// The target of the attached property. This will be set to the parent of the attached property container.
        /// </summary>
        protected MC.BindableObject Target { get; private set; }

        public abstract void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
        public abstract void Remove();

        public void SetParent(object parentElement)
        {
            Target = (MC.BindableObject)parentElement;
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.
        public MC.Element ElementControl => null;
        public object TargetElement => null;
        public bool IsParented() => false;
        public bool IsParentedTo(MC.Element parent) => false;

        public void SetParent(MC.Element parent)
        {
            // This should never get called. Instead, INonPhysicalChild.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }
    }
}
