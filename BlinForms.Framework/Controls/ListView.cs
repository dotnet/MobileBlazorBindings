using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class ListView : FormsComponentBase
    {
        static ListView()
        {
            BlontrolAdapter.KnownElements.Add(typeof(ListView).FullName, new ComponentControlFactoryFunc((_, __) => new BlazorListView()));
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool? FullRowSelect { get; set; }
        [Parameter] public bool? GridLines { get; set; }
        [Parameter] public ColumnHeaderStyle? HeaderStyle { get; set; }
        [Parameter] public bool? MultiSelect { get; set; }
        [Parameter] public bool? ShowGroups { get; set; }
        [Parameter] public bool? UseCompatibleStateImageBehavior { get; set; }
        [Parameter] public View? View { get; set; }
        [Parameter] public ColumnHeader[] Columns { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            if (FullRowSelect != null)
            {
                builder.AddAttribute(1, nameof(FullRowSelect), FullRowSelect.Value);
            }
            if (GridLines != null)
            {
                builder.AddAttribute(2, nameof(GridLines), GridLines.Value);
            }
            if (HeaderStyle != null)
            {
                builder.AddAttribute(3, nameof(HeaderStyle), (int)HeaderStyle.Value);
            }
            if (MultiSelect != null)
            {
                builder.AddAttribute(4, nameof(MultiSelect), MultiSelect.Value);
            }
            if (ShowGroups != null)
            {
                builder.AddAttribute(5, nameof(ShowGroups), ShowGroups.Value);
            }
            if (UseCompatibleStateImageBehavior != null)
            {
                builder.AddAttribute(6, nameof(UseCompatibleStateImageBehavior), UseCompatibleStateImageBehavior.Value);
            }
            if (View != null)
            {
                builder.AddAttribute(7, nameof(View), (int)View.Value);
            }
            if (Columns != null)
            {
                builder.AddAttribute(8, nameof(Columns), System.Text.Json.JsonSerializer.Serialize(Columns));
            }

            //builder.OpenComponent<CascadingValue<Foo>>(9);
            //builder.AddAttribute(1, nameof(CascadingValue<Foo>.Value), FooProp);
            //builder.CloseComponent();
        }

        protected override void RenderContents(RenderTreeBuilder builder)
        {
            builder.AddContent(1000, ChildContent);
        }

        class BlazorListView : System.Windows.Forms.ListView, IBlazorNativeControl
        {
            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(FullRowSelect):
                        FullRowSelect = (bool)attributeValue;
                        break;
                    case nameof(GridLines):
                        GridLines = (bool)attributeValue;
                        break;
                    case nameof(HeaderStyle):
                        HeaderStyle = (ColumnHeaderStyle)int.Parse((string)attributeValue);
                        break;
                    case nameof(MultiSelect):
                        MultiSelect = (bool)attributeValue;
                        break;
                    case nameof(ShowGroups):
                        ShowGroups = (bool)attributeValue;
                        break;
                    case nameof(UseCompatibleStateImageBehavior):
                        UseCompatibleStateImageBehavior = (bool)attributeValue;
                        break;
                    case nameof(View):
                        View = (View)int.Parse((string)attributeValue);
                        break;
                    case nameof(Columns):
                        var x = System.Text.Json.JsonSerializer.Deserialize<ColumnHeader[]>((string)attributeValue);
                        Columns.AddRange(x);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
