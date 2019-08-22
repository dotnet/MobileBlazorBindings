using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public abstract class FormsComponentBase : ComponentBase
    {
        [Parameter] public int? Top { get; set; }
        [Parameter] public int? Left { get; set; }
        [Parameter] public int? Width { get; set; }
        [Parameter] public int? Height { get; set; }

        [Parameter] public int? TabIndex { get; set; }

        [Parameter] public bool? Visible { get; set; }
        [Parameter] public Color? BackColor { get; set; }

        [Parameter] public AnchorStyles? Anchor { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, GetType().FullName);

            RenderAttributes(new AttributesBuilder(builder));

            if (Top != null)
            {
                builder.AddAttribute(100, nameof(Top), Top.Value);
            }
            if (Left != null)
            {
                builder.AddAttribute(101, nameof(Left), Left.Value);
            }
            if (Width != null)
            {
                builder.AddAttribute(102, nameof(Width), Width.Value);
            }
            if (Height != null)
            {
                builder.AddAttribute(103, nameof(Height), Height.Value);
            }
            if (Visible != null)
            {
                builder.AddAttribute(104, nameof(Visible), Visible.Value);
            }
            if (BackColor != null)
            {
                builder.AddAttribute(105, nameof(BackColor), BackColor.Value.ToArgb());
            }
            if (TabIndex != null)
            {
                builder.AddAttribute(106, nameof(TabIndex), TabIndex.Value);
            }
            if (Anchor != null)
            {
                builder.AddAttribute(107, nameof(Anchor), (int)Anchor.Value);
            }

            RenderContents(builder);

            builder.CloseElement();
        }

        protected virtual void RenderAttributes(AttributesBuilder builder)
        {
        }

        /// <summary>
        /// Rendered contents should use sequence values 1000+.
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void RenderContents(RenderTreeBuilder builder)
        {
        }

        public static void ApplyAttribute(Control control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(Top):
                    control.Top = int.Parse((string)attributeValue);
                    break;
                case nameof(Left):
                    control.Left = int.Parse((string)attributeValue);
                    break;
                case nameof(Width):
                    control.Width = int.Parse((string)attributeValue);
                    break;
                case nameof(Height):
                    control.Height = int.Parse((string)attributeValue);
                    break;
                case nameof(Visible):
                    control.Visible = (bool)attributeValue;
                    break;
                case nameof(BackColor):
                    control.BackColor = Color.FromArgb(argb: int.Parse((string)attributeValue));
                    break;
                case nameof(TabIndex):
                    control.TabIndex = int.Parse((string)attributeValue);
                    break;
                case nameof(Anchor):
                    control.Anchor = (AnchorStyles)int.Parse((string)attributeValue);
                    break;
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
