// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlexLayout
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.AlignSelf",
                (element, value) => MC.FlexLayout.SetAlignSelf(element, (FlexAlignSelf)AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Grow",
                (element, value) => MC.FlexLayout.SetGrow(element, AttributeHelper.StringToSingle((string)value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Shrink",
                (element, value) => MC.FlexLayout.SetShrink(element, AttributeHelper.StringToSingle((string)value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Order",
                (element, value) => MC.FlexLayout.SetOrder(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Basis",
                (element, value) => MC.FlexLayout.SetBasis(element, AttributeHelper.StringToFlexBasis(value)));
        }
    }
}
