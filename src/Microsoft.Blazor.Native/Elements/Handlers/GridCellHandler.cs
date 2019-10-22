using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class GridCellHandler : IXamarinFormsElementHandler
    {
        private XF.Grid _parentGrid;
        private XF.View _childView;

        public GridCellHandler(EmblazonRenderer renderer, GridCellPlaceholderElement gridCellPlaceholderElementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            GridCellPlaceholderElementControl = gridCellPlaceholderElementControl ?? throw new ArgumentNullException(nameof(gridCellPlaceholderElementControl));
        }

        public EmblazonRenderer Renderer { get; }
        public GridCellPlaceholderElement GridCellPlaceholderElementControl { get; }
        public XF.Element ElementControl => GridCellPlaceholderElementControl;
        public object TargetElement => ElementControl;

        public int? Column { get; set; }
        public int? ColumnSpan { get; set; }
        public int? Row { get; set; }
        public int? RowSpan { get; set; }

        public XF.Grid ParentGrid
        {
            get => _parentGrid;
            set
            {
                _parentGrid = value;
                AddGridChildIfPossible();
            }
        }

        public XF.View ChildView
        {
            get => _childView;
            set
            {
                _childView = value;
                AddGridChildIfPossible();
            }
        }

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

        private void AddGridChildIfPossible()
        {
            if (ParentGrid != null && ChildView != null)
            {
                ParentGrid.Children.Add(
                    view: ChildView,
                    left: (Column ?? 0),
                    right: (Column ?? 0) + (ColumnSpan ?? 1),
                    top: (Row ?? 0),
                    bottom: (Row ?? 0) + (RowSpan ?? 1));
            }
        }
    }
}
