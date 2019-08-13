using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public abstract class FormsComponentBase : ComponentBase
    {
        [Parameter] public int Top { get; set; }
        [Parameter] public int Left { get; set; }
        [Parameter] public int Width { get; set; }
        [Parameter] public int Height { get; set; }

        [Parameter] public bool Visible { get; set; }
        [Parameter] public Color BackColor { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, GetType().FullName);
            RenderAttributes(builder);
            builder.AddAttribute(100, nameof(Top), Top);
            builder.AddAttribute(101, nameof(Left), Left);
            builder.AddAttribute(102, nameof(Width), Width);
            builder.AddAttribute(103, nameof(Height), Height);
            builder.AddAttribute(104, nameof(Visible), Visible);
            builder.AddAttribute(105, nameof(BackColor), BackColor.ToArgb());
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
                case nameof(Visible):
                    control.Visible = (bool)attributeValue;
                    break;
                case nameof(BackColor):
                    control.BackColor = Color.FromArgb(argb: int.Parse((string)attributeValue));
                    break;
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
