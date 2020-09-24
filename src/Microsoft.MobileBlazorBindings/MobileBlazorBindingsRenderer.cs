// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings
{
    public class MobileBlazorBindingsRenderer : NativeComponentRenderer
    {
        public MobileBlazorBindingsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher { get; } = new XamarinDeviceDispatcher();

        protected override void HandleException(Exception exception)
        {
            ErrorPageHelper.ShowExceptionPage(exception);
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new MobileBlazorBindingsElementManager();
        }
    }
}
