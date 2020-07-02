// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class BlinFormsRenderer : NativeComponentRenderer
    {
        public BlinFormsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        protected override void HandleException(Exception exception)
        {
            MessageBox.Show(exception?.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new BlinFormsElementManager();
        }
    }
}
