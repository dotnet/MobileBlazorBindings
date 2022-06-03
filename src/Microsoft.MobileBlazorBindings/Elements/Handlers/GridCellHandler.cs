// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GridCellHandler : IXamarinFormsContainerElementHandler, INonChildContainerElement
    {
        private readonly List<XF.View> _children = new List<XF.View>();
        private XF.Grid _parentGrid;

        public GridCellHandler(NativeComponentRenderer renderer, GridCellPlaceholderElement gridCellPlaceholderElementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            GridCellPlaceholderElementControl = gridCellPlaceholderElementControl ?? throw new ArgumentNullException(nameof(gridCellPlaceholderElementControl));
        }

        public NativeComponentRenderer Renderer { get; }
        public GridCellPlaceholderElement GridCellPlaceholderElementControl { get; }
        public XF.Element ElementControl => GridCellPlaceholderElementControl;
        public object TargetElement => ElementControl;

        public int Column { get; set; }
        public int ColumnSpan { get; set; } = 1;
        public int Row { get; set; }
        public int RowSpan { get; set; } = 1;

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(GridCell.Column):
                    Column = AttributeHelper.GetInt(attributeValue);
                    _children.ForEach(c => XF.Grid.SetColumn(c, Column));
                    break;
                case nameof(GridCell.ColumnSpan):
                    ColumnSpan = AttributeHelper.GetInt(attributeValue, 1);
                    _children.ForEach(c => XF.Grid.SetColumnSpan(c, ColumnSpan));
                    break;
                case nameof(GridCell.Row):
                    Row = AttributeHelper.GetInt(attributeValue);
                    _children.ForEach(c => XF.Grid.SetRow(c, Row));
                    break;
                case nameof(GridCell.RowSpan):
                    RowSpan = AttributeHelper.GetInt(attributeValue, 1);
                    _children.ForEach(c => XF.Grid.SetRowSpan(c, RowSpan));
                    break;
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (!(child is XF.View childView))
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(XF.View).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));
            }

            XF.Grid.SetColumn(childView, Column);
            XF.Grid.SetColumnSpan(childView, ColumnSpan);
            XF.Grid.SetRow(childView, Row);
            XF.Grid.SetRowSpan(childView, RowSpan);

            _children.Add(childView);
            _parentGrid.Children.Add(childView);
        }

        public void RemoveChild(XF.Element child)
        {
            if (!(child is XF.View childView))
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(XF.View).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));
            }

            _children.Remove(childView);
            _parentGrid.Children.Remove(childView);
        }

        public int GetChildIndex(XF.Element child)
        {
            if (!(child is XF.View childView))
            {
                return -1;
            }

            return _children.IndexOf(childView);
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

        public void SetParent(object parentElement)
        {
            if (!(parentElement is XF.Grid parentGrid))
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(XF.Grid).FullName} but it is of type {parentElement?.GetType().FullName}.", nameof(parentElement));
            }

            _parentGrid = parentGrid;
        }

        public void Remove()
        {
            if (_parentGrid != null)
            {
                foreach (var child in _children)
                {
                    _parentGrid.Children.Remove(child);
                }

                _children.Clear();
                _parentGrid = null;
            }
        }
    }
}
