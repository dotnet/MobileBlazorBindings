using System.ComponentModel;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.GridInternals
{
    internal class ColumnDefinitionMetadata : INotifyPropertyChanged
    {
        private double? _width;
        private XF.GridUnitType? _gridUnitType;

        public double? Width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Width)));
                }
            }
        }
        public XF.GridUnitType? GridUnitType
        {
            get => _gridUnitType;
            set
            {
                if (_gridUnitType != value)
                {
                    _gridUnitType = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GridUnitType)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
