// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GridCellHandler : IXamarinFormsContainerElementHandler, INonChildContainerElement
    {
        public GridCellHandler(NativeComponentRenderer renderer, GridCellPlaceholderElement gridCellPlaceholderElementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            GridCellPlaceholderElementControl = gridCellPlaceholderElementControl ?? throw new ArgumentNullException(nameof(gridCellPlaceholderElementControl));

            _parentChildManager = new ParentChildManager<XF.Grid, XF.View>(AddChildViewToParentGrid);
        }

        public NativeComponentRenderer Renderer { get; }
        public GridCellPlaceholderElement GridCellPlaceholderElementControl { get; }
        public XF.Element ElementControl => GridCellPlaceholderElementControl;
        public object TargetElement => ElementControl;

        public int? Column { get; set; }
        public int? ColumnSpan { get; set; }
        public int? Row { get; set; }
        public int? RowSpan { get; set; }

        private readonly ParentChildManager<XF.Grid, XF.View> _parentChildManager;

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

        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            _parentChildManager.SetChild(child);
        }

        public void RemoveChild(XF.Element child)
        {
            // TODO: This could probably be implemented at some point, but it isn't needed right now
            throw new NotImplementedException();
        }

        public int GetChildIndex(XF.Element child)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return 0;
        }

        public bool IsParented()
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public bool IsParentedTo(XF.Element parent)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public void SetParent(XF.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
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

        public void SetParent(object parentElement)
        {
            _parentChildManager.SetParent((XF.Element)parentElement);
        }
    }
}
