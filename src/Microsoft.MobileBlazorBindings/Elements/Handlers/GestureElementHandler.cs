// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GestureElementHandler : ElementHandler
    {
        public override bool ApplyAdditionalAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(GestureElement.GestureRecognizers):
                    GestureElementControl.GestureRecognizers.Clear();
                    if (attributeValue != null)
                    {
                        ((AttributeValueHolder)attributeValue)(out object value);
                        var gestures = ((IList<XF.IGestureRecognizer>)value);
                        foreach (var gesture in gestures)
                        {
                            GestureElementControl.GestureRecognizers.Add(gesture);
                        }
                    }
                    return true;
                default:
                    return base.ApplyAdditionalAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
