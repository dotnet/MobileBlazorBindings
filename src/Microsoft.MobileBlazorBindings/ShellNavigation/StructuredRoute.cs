using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MobileBlazorBindingsXaminals.ShellNavigation
{
    //Used to map blazor route syntax to forms query syntax
    public class StructuredRoute
    {
        public string OriginalUri { get; }//Full route as it is registered in the razor component
        public string BaseUri { get; }//The route with all the parameters chopped off
        public int ParameterCount => ParameterKeys == null ? 0 : ParameterKeys.Count;

        public List<string> ParameterKeys { get; } = new List<string>();


        public Type Type { get; }

        public StructuredRoute(string originalRoute, Type type)
        {
            if(originalRoute == null)
            {
                throw new ArgumentNullException(nameof(originalRoute));
            }

            OriginalUri = originalRoute;
            Type = type;
            var allRouteSegments = originalRoute.Split('/');
            var parameterKeys = allRouteSegments.Where(x => x.Contains('{') && x.Contains('}'));

            var constantSegments = allRouteSegments.Except(parameterKeys);

            var baseRoute = string.Join("/", constantSegments);

            BaseUri = baseRoute;
            if(parameterKeys.Any())
            {
                ParameterKeys = parameterKeys.Select(x => x.Trim('{').Trim('}')).ToList();
            }
        }

        internal static StructuredRouteResult FindBestMatch(string uri, List<StructuredRoute> routes)
        {
            var match = routes.FirstOrDefault(x => x.BaseUri == uri);
            if (match != null && match.ParameterCount == 0)
                return new StructuredRouteResult(match);

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
                    break;
                parameterCount++;
            }

            parameters.Reverse();
            return new StructuredRouteResult(match,parameters);
        }
    }

    public class StructuredRouteResult 
    {
        public StructuredRoute Route { get; }
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        public StructuredRouteResult(StructuredRoute match)
        {
            Route = match;
        }

        public StructuredRouteResult(StructuredRoute match, List<string> parameters)
        {
            Route = match;
            for (int i =0; i < match.ParameterKeys.Count; i++)
            {
                Parameters[match.ParameterKeys[i]] = parameters.ElementAtOrDefault(i);
            }
        }
    }
}
