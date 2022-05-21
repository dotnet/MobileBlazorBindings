// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Converters;
using Microsoft.Maui.Layouts;

namespace BlazorBindings.Maui.Elements
{
    public partial class AttributeHelper
    {
        private static readonly FlexBasisTypeConverter _flexBasisConverter = new();

        public static string FlexBasisToString(FlexBasis flexBasis)
        {
            return _flexBasisConverter.ConvertToInvariantString(flexBasis);
        }

        public static FlexBasis StringToFlexBasis(object flexBasisString)
        {
            return (FlexBasis)_flexBasisConverter.ConvertFromInvariantString((string)flexBasisString);
        }
    }
}
