﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings
{
    public class MobileBlazorBindingsRenderer : NativeComponentRenderer
    {
        public MobileBlazorBindingsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher { get; } = new XamarinDeviceDispatcher();

        public Task<TComponent> AddComponent<TComponent>(MC.Element parent, Dictionary<string, object> parameters = null) where TComponent : IComponent
        {
            if (parent is MC.Application app)
            {
                app.MainPage ??= new MC.ContentPage();
            }

            var handler = CreateHandler(parent, this);
            return AddComponent<TComponent>(handler, parameters);
        }

        protected override void HandleException(Exception exception)
        {
            ErrorPageHelper.ShowExceptionPage(exception);
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new MobileBlazorBindingsElementManager();
        }

        private static ElementHandler CreateHandler(MC.Element parent, MobileBlazorBindingsRenderer renderer)
        {
            return parent switch
            {
                MC.ContentPage contentPage => new ContentPageHandler(renderer, contentPage),
                MC.ContentView contentView => new ContentViewHandler(renderer, contentView),
                MC.Label label => new LabelHandler(renderer, label),
                MC.FlyoutPage flyoutPage => new FlyoutPageHandler(renderer, flyoutPage),
                MC.ScrollView scrollView => new ScrollViewHandler(renderer, scrollView),
                MC.ShellContent shellContent => new ShellContentHandler(renderer, shellContent),
                MC.Shell shell => new ShellHandler(renderer, shell),
                MC.ShellItem shellItem => new ShellItemHandler(renderer, shellItem),
                MC.ShellSection shellSection => new ShellSectionHandler(renderer, shellSection),
                MC.TabbedPage tabbedPage => new TabbedPageHandler(renderer, tabbedPage),
                _ => new ElementHandler(renderer, parent),
            };
        }
    }
}
