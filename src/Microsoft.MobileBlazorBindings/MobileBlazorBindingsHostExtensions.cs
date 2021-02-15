// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using System.Threading.Tasks;
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

#pragma warning disable CA2000 // Dispose objects before losing scope
            var renderer = new MobileBlazorBindingsRenderer(services, services.GetRequiredService<ILoggerFactory>());
#pragma warning restore CA2000 // Dispose objects before losing scope

            // TODO: This call is an async call, but is called as "fire-and-forget," which is not ideal.
            // We need to figure out how to get Xamarin.Forms to run this startup code asynchronously, which
            // is how this method should be called.
            renderer.AddComponent<TComponent>(CreateHandler(parent, renderer)).ConfigureAwait(false);
        }

        //Slight tweak on the above method to make it async and allow use with a serviceProvider instead of a host, and with a type argument instead of generic argument so it can be called at run time
        //The above methos only needs host for it's services so it could be refactored.
        //The above method only needs <t> so it can be used as a type in an overload of the method it calls.
        //This version also allows an optional set of parameters
        //The only downside is you can't have design/compiletime type safety
        //There's a lot of duplicate code between the two, can probably refactor the core of the method into a separate method that they both call
        public static async Task<IComponent> AddComponent(this IServiceProvider services, XF.Element parent, Type type, System.Collections.Generic.Dictionary<string, string> parameters = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (!typeof(IComponent).IsAssignableFrom(type))
            {
                throw new InvalidOperationException($"Cannot add {type.Name} to {parent.GetType().Name}. {type.Name} is not an IComponent. If you are trying to add a Xamarin.Forms type, try adding the Mobile Blazor Bindings equivalent instead.");
            }

#pragma warning disable CA2000 // Dispose objects before losing scope
            var renderer = new MobileBlazorBindingsRenderer(services, services.GetRequiredService<ILoggerFactory>());
#pragma warning restore CA2000 // Dispose objects before losing scope

            return await renderer.AddComponent(type, CreateHandler(parent, renderer), parameters).ConfigureAwait(false);
        }

        private static ElementHandler CreateHandler(XF.Element parent, MobileBlazorBindingsRenderer renderer)
        {
            return parent switch
            {
                XF.ContentPage contentPage => new ContentPageHandler(renderer, contentPage),
                XF.ContentView contentView => new ContentViewHandler(renderer, contentView),
                XF.Label label => new LabelHandler(renderer, label),
                XF.FlyoutPage flyoutPage => new FlyoutPageHandler(renderer, flyoutPage),
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