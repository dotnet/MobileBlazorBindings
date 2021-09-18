// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class CollectionView<T> : GroupableItemsView<T>
    {
        static CollectionView()
        {
            ElementHandlerRegistry.RegisterElementHandler<CollectionView<T>>(
                renderer => new CollectionViewHandler(renderer, new XF.CollectionView()));
        }

        public new XF.CollectionView NativeControl => ((CollectionViewHandler)ElementHandler).CollectionViewControl;
    }
}
