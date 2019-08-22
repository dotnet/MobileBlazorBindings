using Emblazon;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public abstract class FormsComponentBase : NativeControlComponentBase
    {
        [Parameter] public int? Top { get; set; }
        [Parameter] public int? Left { get; set; }
        [Parameter] public int? Width { get; set; }
        [Parameter] public int? Height { get; set; }

        [Parameter] public int? TabIndex { get; set; }

        [Parameter] public bool? Visible { get; set; }
        [Parameter] public Color? BackColor { get; set; }

        [Parameter] public AnchorStyles? Anchor { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Top != null)
            {
                builder.AddAttribute(nameof(Top), Top.Value);
            }
            if (Left != null)
            {
                builder.AddAttribute(nameof(Left), Left.Value);
            }
            if (Width != null)
            {
                builder.AddAttribute(nameof(Width), Width.Value);
            }
            if (Height != null)
            {
                builder.AddAttribute(nameof(Height), Height.Value);
            }
            if (Visible != null)
            {
                builder.AddAttribute(nameof(Visible), Visible.Value);
            }
            if (BackColor != null)
            {
                builder.AddAttribute(nameof(BackColor), BackColor.Value.ToArgb());
            }
            if (TabIndex != null)
            {
                builder.AddAttribute(nameof(TabIndex), TabIndex.Value);
            }
            if (Anchor != null)
            {
                builder.AddAttribute(nameof(Anchor), (int)Anchor.Value);
            }
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
