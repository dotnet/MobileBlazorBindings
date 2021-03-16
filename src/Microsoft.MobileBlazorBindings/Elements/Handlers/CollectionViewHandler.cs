// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class CollectionViewHandler : GroupableItemsViewHandler
    {
        public CollectionViewHandler(NativeComponentRenderer renderer, XF.CollectionView collectionViewControl) : base(renderer, collectionViewControl)
        {
            CollectionViewControl = collectionViewControl ?? throw new ArgumentNullException(nameof(collectionViewControl));
        }

        public XF.CollectionView CollectionViewControl { get; }
    }
}
