// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract class BaseAttachedPropertiesHandler : IXamarinFormsElementHandler, INonPhysicalChild
    {
        /// <summary>
        /// The target of the attached property. This will be set to the parent of the attached property container.
        /// </summary>
        protected XF.BindableObject Target { get; private set; }

        public abstract void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);

        public void SetParent(object parentElement)
        {
            Target = (XF.BindableObject)parentElement;
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.
        public XF.Element ElementControl => null;
        public object TargetElement => null;
        public bool IsParented() => false;
        public bool IsParentedTo(XF.Element parent) => false;

        public void SetParent(XF.Element parent)
        {
            // This should never get called. Instead, INonPhysicalChild.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }
    }
}
