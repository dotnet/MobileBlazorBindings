using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class FormattedStringHandler : ElementHandler
    {
        public FormattedStringHandler(NativeComponentRenderer renderer, XF.FormattedString formattedStringControl) : base(renderer, formattedStringControl)
        {
            FormattedStringControl = formattedStringControl ?? throw new ArgumentNullException(nameof(formattedStringControl));
        }

        public XF.FormattedString FormattedStringControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
