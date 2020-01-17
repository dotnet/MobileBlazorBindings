// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TemplatedView : Layout
    {
        // TODO: Look in the future how to support a ControlTemplate:
        // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/templates/control-templates/creating

        public new XF.TemplatedView NativeControl => ((TemplatedViewHandler)ElementHandler).TemplatedViewControl;
    }
}
