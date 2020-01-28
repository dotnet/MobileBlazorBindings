﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ContentView : TemplatedView
    {
        static ContentView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ContentView>(
                renderer => new ContentViewHandler(renderer, new XF.ContentView()));
        }

        public new XF.ContentView NativeControl => ((ContentViewHandler)ElementHandler).ContentViewControl;
    }
}
