// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ProgressBarHandler : ViewHandler
    {
        public ProgressBarHandler(NativeComponentRenderer renderer, XF.ProgressBar progressBarControl) : base(renderer, progressBarControl)
        {
            ProgressBarControl = progressBarControl ?? throw new ArgumentNullException(nameof(progressBarControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ProgressBar ProgressBarControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ProgressBar.Progress):
                    ProgressBarControl.Progress = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.ProgressBar.ProgressColor):
                    ProgressBarControl.ProgressColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
