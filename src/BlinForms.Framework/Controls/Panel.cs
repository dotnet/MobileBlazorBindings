// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class Panel : FormsComponentBase
    {
        static Panel()
        {
            ElementHandlerRegistry.RegisterElementHandler<Panel, BlazorPanel>();
        }

        [Parameter] public bool? AutoScroll { get; set; }
#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutoScroll != null)
            {
                builder.AddAttribute(nameof(AutoScroll), AutoScroll.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        private class BlazorPanel : System.Windows.Forms.Panel, IWindowsFormsControlHandler
        {
            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(AutoScroll):
                        AutoScroll = AttributeHelper.GetBool(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
