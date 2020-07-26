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
        public string OriginalUri { get;  set; }//Full route as it is registered in the razor component
        public string BaseUri { get; set; }//The route with all the parameters chopped off
        public int ParameterCount => ParameterKeys == null ? 0 : ParameterKeys.Count;


        public List<string> ParameterKeys { get; set; } = new List<string>();


        public Type Type { get; }

        public StructuredRoute(string originalRoute, Type type)
        {
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

        public StructuredRoute()
        {

        }


        internal static StructuredRouteResult FindBestMatch(string uri, List<StructuredRoute> routes)
        {
            var match = routes.FirstOrDefault(x => x.BaseUri == uri);
            if (match != null && match.ParameterCount == 0)
                return new StructuredRouteResult(match);

            var pieces = uri.Split('/').ToList();
            var parameter = pieces.LastOrDefault();
            pieces.Remove(parameter);


            //Assume 1 parameter
            //Update later to allow two 
            var uriWithoutParameter = string.Join("/", pieces);

            match = routes.FirstOrDefault(x => x.BaseUri == uriWithoutParameter &&   x.ParameterCount == 1);

            return new StructuredRouteResult(match, new List<string> { parameter });
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
