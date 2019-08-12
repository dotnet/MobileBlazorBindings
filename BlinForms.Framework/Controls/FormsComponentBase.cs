using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public abstract class FormsComponentBase : ComponentBase
    {
        [Parameter] public int Top { get; set; }
        [Parameter] public int Left { get; set; }
        [Parameter] public int Width { get; set; }
        [Parameter] public int Height { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, GetType().FullName);
            RenderAttributes(builder);
            builder.AddAttribute(100, nameof(Top), Top);
            builder.AddAttribute(200, nameof(Left), Left);
            builder.AddAttribute(300, nameof(Width), Width);
            builder.AddAttribute(400, nameof(Height), Height);
            RenderContents(builder);
            builder.CloseElement();
        }

        protected abstract void RenderAttributes(RenderTreeBuilder builder);

        protected virtual void RenderContents(RenderTreeBuilder builder)
        {
        }

        public static void ApplyAttribute(Control control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                // TODO: Fix not setting default values for types. Maybe use nullable types on components?
                case nameof(Top):
                    if ((attributeValue as string) != "0")
                        control.Top = int.Parse((string)attributeValue);
                    break;
                case nameof(Left):
                    if ((attributeValue as string) != "0")
                        control.Left = int.Parse((string)attributeValue);
                    break;
                case nameof(Width):
                    if ((attributeValue as string) != "0")
                        control.Width = int.Parse((string)attributeValue);
                    break;
                case nameof(Height):
                    if ((attributeValue as string) != "0")
                        control.Height = int.Parse((string)attributeValue);
                    break;
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
