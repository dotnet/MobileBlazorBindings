// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements
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
