// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class InputViewHandler : ViewHandler
    {
        public InputViewHandler(NativeComponentRenderer renderer, XF.InputView inputViewControl) : base(renderer, inputViewControl)
        {
            InputViewControl = inputViewControl ?? throw new System.ArgumentNullException(nameof(inputViewControl));
        }

        public XF.InputView InputViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.InputView.IsReadOnly):
                    InputViewControl.IsReadOnly = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.InputView.IsSpellCheckEnabled):
                    InputViewControl.IsSpellCheckEnabled = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.InputView.MaxLength):
                    InputViewControl.MaxLength = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
