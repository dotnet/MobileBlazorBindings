﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Microsoft.MobileBlazorBindings.ShellNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings
{
    public class ShellNavigationManager //: NavigationManager I would have liked to inherit from NavigationManager but I can't work out what URIs to initialize it with
    {
        private readonly IServiceProvider _services;
        private readonly List<StructuredRoute> Routes = new List<StructuredRoute>();
        private readonly Dictionary<string, MBBRouteFactory> RouteFactories = new Dictionary<string, MBBRouteFactory>();
        private readonly Dictionary<Type, StructuredRouteResult> NavigationParameters = new Dictionary<Type, StructuredRouteResult>();

        public ShellNavigationManager(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            FindRoutes();
        }

        //TODO This route matching could be better. Can we use the ASPNEt version?
        private void FindRoutes()
        {
            var assembly = MC.Application.Current.GetType().Assembly;
            var pages = assembly.GetTypes().Where(x => x.GetCustomAttributes<RouteAttribute>().Any());//TODO: Could this be more efficient if it only looked for classes that are razor components? Or maybe thats an extra step that would slow things down. Profiler required.
            foreach (var page in pages)
            {
                //Find each @page on a page. There can be multiple.
                var routes = page.GetCustomAttributes<RouteAttribute>();
                foreach (var route in routes)
                {
                    if (route.Template == "/")
                    {
                        // This route can be used in Hybrid apps and should be ignored by Shell (because Shell doesn't support empty routes anyway)
                        continue;
                    }

                    if (page.IsSubclassOf(typeof(ComponentBase)))
                    {
                        var structuredRoute = new StructuredRoute(route.Template, page);

                        //Register with XamarinForms so it can handle Navigation.
                        var routeFactory = new MBBRouteFactory(page, this);
                        MC.Routing.RegisterRoute(structuredRoute.BaseUri, routeFactory);

                        //Also register route in our own list for setting parameters and tracking if it is registered;
                        Routes.Add(structuredRoute);
                        RouteFactories[structuredRoute.BaseUri] = routeFactory;
                    }
                    else
                    {
                        throw new NotImplementedException($"Page directive is invalid on type: {page} because it does not inherit from ComponentBase. Only Components can be used for page routing.");
                    }
                }
            }
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void NavigateTo(string uri, Dictionary<string, object> parameters = null)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _ = NavigateToAsync(uri, parameters);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public async Task NavigateToAsync(string uri, Dictionary<string, object> parameters = null)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var route = StructuredRoute.FindBestMatch(uri, Routes, parameters);

            if (route != null)
            {
                NavigationParameters[route.Route.Type] = route;
                if (!RouteFactories.TryGetValue(route.Route.BaseUri, out var routeFactory))
                {
                    throw new InvalidOperationException($"A route factory for URI '{uri}' could not be found. It should have been registered automatically in the {nameof(ShellNavigationManager)} constructor.");
                }
                await routeFactory.CreateAsync().ConfigureAwait(true);
                await MC.Shell.Current.GoToAsync(route.Route.BaseUri).ConfigureAwait(false);
            }
            else
            {
                throw new InvalidOperationException($"The route '{uri}' is not registered. Register page routes using the '@page' directive in the page.");
            }
        }

        internal async Task<MC.Page> BuildPage(Type componentType)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope. Scope is disposed when page is closed.
            var scope = _services.CreateScope();
#pragma warning restore CA2000 // Dispose objects before losing scope
            var serviceProvider = scope.ServiceProvider;

            var container = new RootContainerHandler();
            var route = NavigationParameters[componentType];

            var renderer = serviceProvider.GetRequiredService<MobileBlazorBindingsRenderer>();

            var parameters = ConvertParameters(componentType, route.PathParameters);

            if (route.AdditionalParameters is not null)
            {
                if (parameters is null)
                {
                    parameters = route.AdditionalParameters;
                }
                else
                {
                    foreach (var (key, value) in route.AdditionalParameters)
                    {
                        parameters.Add(key, value);
                    }
                }
            }

            var addComponentTask = renderer.AddComponent(componentType, container, parameters);
            var elementAddedTask = container.WaitForElementAsync();

            await Task.WhenAny(addComponentTask, elementAddedTask).ConfigureAwait(false);

            if (addComponentTask.Exception != null)
            {
                // If any exception ecountered during the rendering - throw it directly instead of wrapping in another exception.
                var exception = addComponentTask.Exception.InnerException;
                ExceptionDispatchInfo.Throw(exception);
            }

            if (container.Elements.Count != 1)
            {
                throw new InvalidOperationException("The target component of a Shell navigation must have exactly one root element.");
            }

            var page = container.Elements.FirstOrDefault() as MC.Page
                ?? throw new InvalidOperationException("The target component of a Shell navigation must derive from the Page component.");

            page.ParentChanged += DisposeScopeWhenParentRemoved;

            return page;

            void DisposeScopeWhenParentRemoved(object _, EventArgs __)
            {
                if (page.Parent is null)
                {
                    scope.Dispose();
                    page.ParentChanged -= DisposeScopeWhenParentRemoved;
                }
            }
        }

        internal static Dictionary<string, object> ConvertParameters(Type componentType, Dictionary<string, string> parameters)
        {
            if (parameters is null)
            {
                return null;
            }

            var convertedParameters = new Dictionary<string, object>();

            foreach (var keyValue in parameters)
            {
                var propertyType = componentType.GetProperty(keyValue.Key)?.PropertyType ?? typeof(string);
                if (!TryParse(propertyType, keyValue.Value, out var parsedValue))
                {
                    throw new InvalidOperationException($"The value {keyValue.Value} can not be converted to a {propertyType.Name}");
                }

                convertedParameters[keyValue.Key] = parsedValue;
            }

            return convertedParameters;
        }

        /// <summary>
        /// Converts a string into the specified type. If conversion was successful, parsed property will be of the correct type and method will return true.
        /// If conversion fails it will return false and parsed property will be null.
        /// This method supports the 8 data types that are valid navigation parameters in Blazor. Passing a string is also safe but will be returned as is because no conversion is neccessary.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="s"></param>
        /// <param name="result">The parsed object of the type specified. This will be null if conversion failed.</param>
        /// <returns>True if s was converted successfully, otherwise false</returns>
        internal static bool TryParse(Type type, string s, out object result)
        {
            bool success;

            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type == typeof(string))
            {
                result = s;
                success = true;
            }
            else if (type == typeof(int))
            {
                success = int.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(Guid))
            {
                success = Guid.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(bool))
            {
                success = bool.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(DateTime))
            {
                success = DateTime.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(decimal))
            {
                success = decimal.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(double))
            {
                success = double.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(float))
            {
                success = float.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(long))
            {
                success = long.TryParse(s, out var parsed);
                result = parsed;
            }
            else
            {
                result = null;
                success = false;
            }
            return success;
        }
    }
}
