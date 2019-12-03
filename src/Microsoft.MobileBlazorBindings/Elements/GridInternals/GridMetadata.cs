using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Microsoft.MobileBlazorBindings.Elements.GridInternals
{
    internal class GridMetadata
    {
        private ObservableCollection<ColumnDefinitionMetadata> _columnDefinitions;
        private ObservableCollection<RowDefinitionMetadata> _rowDefinitions;

        public GridMetadata()
        {
            ColumnDefinitions = new ObservableCollection<ColumnDefinitionMetadata>();
            RowDefinitions = new ObservableCollection<RowDefinitionMetadata>();
        }

        private void ItemDefinitions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        item.PropertyChanged += ItemHasChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged -= ItemHasChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        item.PropertyChanged += ItemHasChanged;
                    }
                    foreach (INotifyPropertyChanged row in e.OldItems)
                    {
                        row.PropertyChanged -= ItemHasChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged -= ItemHasChanged;
                    }
                    break;
            }
            CollectionHasChanged();
        }

        private void ItemHasChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged?.Invoke(this, EventArgs.Empty);
        }

        private void CollectionHasChanged()
        {
            StateHasChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler StateHasChanged;

        public ObservableCollection<ColumnDefinitionMetadata> ColumnDefinitions
        {
            get => _columnDefinitions;
            set
            {
                if (_columnDefinitions != null)
                {
                    _columnDefinitions.CollectionChanged -= ItemDefinitions_CollectionChanged;
                }
                _columnDefinitions = value;
                if (_columnDefinitions != null)
                {
                    _columnDefinitions.CollectionChanged += ItemDefinitions_CollectionChanged;
                }
            }
        }

        public ObservableCollection<RowDefinitionMetadata> RowDefinitions
        {
            get => _rowDefinitions;
            set
            {
                if (_rowDefinitions != null)
                {
                    _rowDefinitions.CollectionChanged -= ItemDefinitions_CollectionChanged;
                }
                _rowDefinitions = value;
                if (_rowDefinitions != null)
                {
                    _rowDefinitions.CollectionChanged += ItemDefinitions_CollectionChanged;
                }
            }
        }
    }
}
