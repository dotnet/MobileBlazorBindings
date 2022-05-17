// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBindings.Maui.ShellNavigation
{
    //Used to map blazor route syntax to forms query syntax
    internal class StructuredRoute
    {
#pragma warning disable CA1056 // Uri properties should not be strings
        public string OriginalUri { get; }//Full route as it is registered in the razor component
#pragma warning restore CA1056 // Uri properties should not be strings
#pragma warning disable CA1056 // Uri properties should not be strings
        public string BaseUri { get; }//The route with all the parameters chopped off
#pragma warning restore CA1056 // Uri properties should not be strings
        public int ParameterCount => ParameterKeys == null ? 0 : ParameterKeys.Count;

        public List<string> ParameterKeys { get; } = new List<string>();


        public Type Type { get; }

        public StructuredRoute(string originalRoute, Type type)
        {
            OriginalUri = originalRoute ?? throw new ArgumentNullException(nameof(originalRoute));
            Type = type;
            var allRouteSegments = originalRoute.Split('/');
            var parameterKeys = allRouteSegments.Where(x => x.Contains('{') && x.Contains('}'));

            var constantSegments = allRouteSegments.Except(parameterKeys);

            var baseRoute = string.Join("/", constantSegments);

            BaseUri = baseRoute;
            if (parameterKeys.Any())
            {
                ParameterKeys = parameterKeys.Select(x => x.Trim('{').Trim('}')).ToList();
            }
        }

        internal static StructuredRouteResult FindBestMatch(string uri, List<StructuredRoute> routes, Dictionary<string, object> additionalParameters)
        {
            var match = routes.FirstOrDefault(x => x.BaseUri == uri);
            if (match != null && match.ParameterCount == 0)
            {
                return new StructuredRouteResult(match, additionalParameters: additionalParameters);
            }

            var pieces = uri.Split('/').ToList();

            var reversedPieces = pieces.Where(x => !string.IsNullOrEmpty(x)).ToList();//make a new copy
            reversedPieces.Reverse();
            var parameters = new List<string>();

            var parameterCount = 1;
            foreach (var piece in reversedPieces)
            {
                uri = uri.Substring(0, uri.Length - piece.Length);
                uri = uri.TrimEnd('/');
                match = routes.FirstOrDefault(x => x.BaseUri == uri && x.ParameterCount == parameterCount);
                parameters.Add(piece);
                if (match != null)
                {
                    break;
                }

                parameterCount++;
            }

            parameters.Reverse();
            return new StructuredRouteResult(match, parameters, additionalParameters);
        }
    }

    internal class StructuredRouteResult
    {
        public StructuredRoute Route { get; }
        public Dictionary<string, string> PathParameters { get; } = new Dictionary<string, string>();
        public Dictionary<string, object> AdditionalParameters { get; }

        public StructuredRouteResult(StructuredRoute match,
            List<string> parameters = null,
            Dictionary<string, object> additionalParameters = null)
        {
            Route = match ?? throw new ArgumentNullException(nameof(match));

            if (parameters is not null)
            {
                for (var i = 0; i < match.ParameterKeys.Count; i++)
                {
                    PathParameters[match.ParameterKeys[i]] = parameters.ElementAtOrDefault(i);
                }
            }

            AdditionalParameters = additionalParameters;
        }
    }
}
