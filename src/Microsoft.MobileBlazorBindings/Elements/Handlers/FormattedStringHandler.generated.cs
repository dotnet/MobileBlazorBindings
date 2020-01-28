// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FormattedStringHandler : ElementHandler
    {
        public FormattedStringHandler(NativeComponentRenderer renderer, XF.FormattedString formattedStringControl) : base(renderer, formattedStringControl)
        {
            FormattedStringControl = formattedStringControl ?? throw new ArgumentNullException(nameof(formattedStringControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.FormattedString FormattedStringControl { get; }
    }
}
