// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class GestureRecognizer : Element, IGestureRecognizer
    {
        public abstract XF.IGestureRecognizer GestureRecognizerControl { get; }
    }



}
