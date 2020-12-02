using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract class BaseAttachedPropertiesHandler : IXamarinFormsElementHandler, INonPhysicalChild
    {
        protected XF.BindableObject Parent { get; private set; }

        public abstract void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);

        public void SetParent(object parentElement)
        {
            Parent = (XF.BindableObject)parentElement;
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.
        public XF.Element ElementControl => null;
        public object TargetElement => null;
        public int GetPhysicalSiblingIndex() => 0;
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
