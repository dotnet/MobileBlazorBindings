// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellGroupItemHandler : BaseShellItemHandler
    {
        public ShellGroupItemHandler(NativeComponentRenderer renderer, XF.ShellGroupItem shellGroupItemControl) : base(renderer, shellGroupItemControl)
        {
            ShellGroupItemControl = shellGroupItemControl ?? throw new ArgumentNullException(nameof(shellGroupItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ShellGroupItem ShellGroupItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ShellGroupItem.FlyoutDisplayOptions):
                    ShellGroupItemControl.FlyoutDisplayOptions = (XF.FlyoutDisplayOptions)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
