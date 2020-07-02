// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class SplitContainer : FormsComponentBase
    {
        static SplitContainer()
        {
            ElementHandlerRegistry.RegisterElementHandler<SplitContainer, BlazorSplitContainer>();
        }

        [Parameter] public RenderFragment Panel1 { get; set; }
        [Parameter] public RenderFragment Panel2 { get; set; }

        [Parameter] public Orientation? Orientation { get; set; }
        [Parameter] public int? SplitterDistance { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (SplitterDistance != null)
            {
                builder.AddAttribute(nameof(SplitterDistance), SplitterDistance.Value);
            }
        }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SplitterPanel1>(0);
            builder.AddAttribute(1, nameof(SplitterPanel1.ChildContent), Panel1);
            builder.CloseComponent();

            builder.OpenComponent<SplitterPanel2>(2);
            builder.AddAttribute(3, nameof(SplitterPanel2.ChildContent), Panel2);
            builder.CloseComponent();
        }

        private class BlazorSplitContainer : System.Windows.Forms.SplitContainer, IWindowsFormsControlHandler
        {
            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Orientation):
                        Orientation = (Orientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(SplitterDistance):
                        SplitterDistance = AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
