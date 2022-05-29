// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class RadioButtonHandler : TemplatedViewHandler
    {
        public override bool ApplyAdditionalAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(RadioButton.Title):
                    RadioButtonControl.Content = (string)attributeValue;
                    return true;

                case nameof(RadioButton.Value):
                    RadioButtonControl.Value = AttributeHelper.DelegateToObject<object>(attributeValue);
                    return true;
            }

            return base.ApplyAdditionalAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }
    }
}
