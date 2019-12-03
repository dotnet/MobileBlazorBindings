using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GridCellHandler : IXamarinFormsElementHandler, IParentChildManagementRequired
    {
        public GridCellHandler(EmblazonRenderer renderer, GridCellPlaceholderElement gridCellPlaceholderElementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            GridCellPlaceholderElementControl = gridCellPlaceholderElementControl ?? throw new ArgumentNullException(nameof(gridCellPlaceholderElementControl));

            ParentChildManager = new ParentChildManager<XF.Grid, XF.View>(AddChildViewToParentGrid);
        }

        public EmblazonRenderer Renderer { get; }
        public GridCellPlaceholderElement GridCellPlaceholderElementControl { get; }
        public XF.Element ElementControl => GridCellPlaceholderElementControl;
        public object TargetElement => ElementControl;

        public int? Column { get; set; }
        public int? ColumnSpan { get; set; }
        public int? Row { get; set; }
        public int? RowSpan { get; set; }

        public IParentChildManager ParentChildManager { get; }

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(GridCell.Column):
                    Column = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(GridCell.ColumnSpan):
                    ColumnSpan = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(GridCell.Row):
                    Row = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(GridCell.RowSpan):
                    RowSpan = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        private void AddChildViewToParentGrid(ParentChildManager<XF.Grid, XF.View> parentChildManager)
        {
            parentChildManager.Parent.Children.Add(
                view: parentChildManager.Child,
                left: (Column ?? 0),
                right: (Column ?? 0) + (ColumnSpan ?? 1),
                top: (Row ?? 0),
                bottom: (Row ?? 0) + (RowSpan ?? 1));
        }
    }
}
