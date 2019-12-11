// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.ComponentModel;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.GridInternals
{
    internal class RowDefinitionMetadata : INotifyPropertyChanged
    {
        private double? _height;
        private XF.GridUnitType? _gridUnitType;

        public double? Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Height)));
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
