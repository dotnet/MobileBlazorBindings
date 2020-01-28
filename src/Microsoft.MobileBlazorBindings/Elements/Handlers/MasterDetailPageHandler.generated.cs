// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class MasterDetailPageHandler : PageHandler
    {
        public MasterDetailPageHandler(NativeComponentRenderer renderer, XF.MasterDetailPage masterDetailPageControl) : base(renderer, masterDetailPageControl)
        {
            MasterDetailPageControl = masterDetailPageControl ?? throw new ArgumentNullException(nameof(masterDetailPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.MasterDetailPage MasterDetailPageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.MasterDetailPage.IsGestureEnabled):
                    MasterDetailPageControl.IsGestureEnabled = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.MasterDetailPage.IsPresented):
                    MasterDetailPageControl.IsPresented = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.MasterDetailPage.MasterBehavior):
                    MasterDetailPageControl.MasterBehavior = (XF.MasterBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
