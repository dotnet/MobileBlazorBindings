// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellGroupItemHandler : BaseShellItemHandler
    {
        private static readonly MC.FlyoutDisplayOptions FlyoutDisplayOptionsDefaultValue = MC.ShellGroupItem.FlyoutDisplayOptionsProperty.DefaultValue is MC.FlyoutDisplayOptions value ? value : default;

        public ShellGroupItemHandler(NativeComponentRenderer renderer, MC.ShellGroupItem shellGroupItemControl) : base(renderer, shellGroupItemControl)
        {
            ShellGroupItemControl = shellGroupItemControl ?? throw new ArgumentNullException(nameof(shellGroupItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ShellGroupItem ShellGroupItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ShellGroupItem.FlyoutDisplayOptions):
                    ShellGroupItemControl.FlyoutDisplayOptions = (MC.FlyoutDisplayOptions)AttributeHelper.GetInt(attributeValue, (int)FlyoutDisplayOptionsDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
