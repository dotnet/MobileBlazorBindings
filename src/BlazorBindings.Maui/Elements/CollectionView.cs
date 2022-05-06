// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class CollectionView<T> : GroupableItemsView<T>
    {
        static CollectionView()
        {
            ElementHandlerRegistry.RegisterElementHandler<CollectionView<T>>(
                renderer => new CollectionViewHandler(renderer, new MC.CollectionView()));
        }

        public new MC.CollectionView NativeControl => ((CollectionViewHandler)ElementHandler).CollectionViewControl;
    }
}
