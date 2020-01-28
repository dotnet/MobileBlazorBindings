// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SwitchHandler : ViewHandler
    {
        public SwitchHandler(NativeComponentRenderer renderer, XF.Switch switchControl) : base(renderer, switchControl)
        {
            SwitchControl = switchControl ?? throw new System.ArgumentNullException(nameof(switchControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Switch SwitchControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (attributeEventHandlerId != 0)
            {
                ApplyEventHandlerId(attributeName, attributeEventHandlerId);
            }

            switch (attributeName)
            {
                case nameof(XF.Switch.IsToggled):
                    SwitchControl.IsToggled = AttributeHelper.GetBool(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId);
    }
}
