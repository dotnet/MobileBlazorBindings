using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public abstract class FormsComponentBase : ComponentBase
    {
        [Parameter] public int Top { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, GetType().FullName);
            RenderAttributes(builder);
            builder.AddAttribute(100, nameof(Top), Top);
            builder.CloseElement();
        }

        protected abstract void RenderAttributes(RenderTreeBuilder builder);

        public static void ApplyAttribute(Control control, ref RenderTreeFrame attributeFrame)
        {
            switch (attributeFrame.AttributeName)
            {
                case nameof(Top):
                    control.Top = int.Parse((string)attributeFrame.AttributeValue);
                    break;
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeFrame.AttributeName}'");
            }
        }
    }
}
