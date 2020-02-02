// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GestureElementHandler
    {
        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.GestureElement.GestureRecognizers):

                    ((AttributeValueHolder)attributeValue)(out object value);
                    var gestures = ((IList<IGestureRecognizer>)value).Select(g => g.GestureRecognizerControl);
                    foreach (var gesture in gestures)
                    {
                        GestureElementControl.GestureRecognizers.Add(gesture);
                    }
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
