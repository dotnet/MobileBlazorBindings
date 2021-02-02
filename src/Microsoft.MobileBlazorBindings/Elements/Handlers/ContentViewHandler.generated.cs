// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ContentViewHandler : TemplatedViewHandler
    {

        public ContentViewHandler(NativeComponentRenderer renderer, XF.ContentView contentViewControl) : base(renderer, contentViewControl)
        {
            ContentViewControl = contentViewControl ?? throw new ArgumentNullException(nameof(contentViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ContentView ContentViewControl { get; }
    }
}
