// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
    internal class MbbDataTemplate<T> : MC.DataTemplate
    {
        public MbbDataTemplate(DataTemplateItemsComponent<T> dataTemplateItemsAccessor)
            : base(() =>
            {
                var itemRootView = new MC.VerticalStackLayout();
                dataTemplateItemsAccessor.Add(itemRootView);
                return itemRootView;
            })
        {
        }
    }
}
