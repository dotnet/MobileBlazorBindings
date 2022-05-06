// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class CollectionViewHandler : GroupableItemsViewHandler
    {
        public CollectionViewHandler(NativeComponentRenderer renderer, MC.CollectionView collectionViewControl) : base(renderer, collectionViewControl)
        {
            CollectionViewControl = collectionViewControl ?? throw new ArgumentNullException(nameof(collectionViewControl));
        }

        public MC.CollectionView CollectionViewControl { get; }
    }
}
