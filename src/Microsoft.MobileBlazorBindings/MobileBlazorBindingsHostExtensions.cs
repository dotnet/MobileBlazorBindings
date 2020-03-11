// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public static class MobileBlazorBindingsHostExtensions
    {
        /// <summary>
        /// Creates a component of type <typeparamref name="TComponent"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="host"></param>
        /// <param name="parent"></param>
        public static void AddComponent<TComponent>(this IHost host, XF.Element parent) where TComponent : IComponent
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            var services = host.Services;
            var renderer = new MobileBlazorBindingsRenderer(services, services.GetRequiredService<ILoggerFactory>());

            // TODO: This call is an async call, but is called as "fire-and-forget," which is not ideal.
            // We need to figure out how to get Xamarin.Forms to run this startup code asynchronously, which
            // is how this method should be called.
            renderer.AddComponent<TComponent>(CreateHandler(parent, renderer)).ConfigureAwait(false);
        }

        private static ElementHandler CreateHandler(XF.Element parent, MobileBlazorBindingsRenderer renderer)
        {
            return parent switch
            {
                XF.ContentPage contentPage => new ContentPageHandler(renderer, contentPage),
                XF.ContentView contentView => new ContentViewHandler(renderer, contentView),
                XF.FormattedString formattedString => new FormattedStringHandler(renderer, formattedString),
                XF.Label label => new LabelHandler(renderer, label),
                XF.MasterDetailPage masterDetailPage => new MasterDetailPageHandler(renderer, masterDetailPage),
                XF.ScrollView scrollView => new ScrollViewHandler(renderer, scrollView),
                XF.ShellContent shellContent => new ShellContentHandler(renderer, shellContent),
                XF.Shell shell => new ShellHandler(renderer, shell),
                XF.ShellItem shellItem => new ShellItemHandler(renderer, shellItem),
                XF.ShellSection shellSection => new ShellSectionHandler(renderer, shellSection),
                XF.TabbedPage tabbedPage => new TabbedPageHandler(renderer, tabbedPage),
                _ => new ElementHandler(renderer, parent),
            };
        }
    }
}
