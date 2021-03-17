using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.MobileBlazorBindings.ComponentGenerator
{
    internal static class GeneratorSettingsParser
    {
        internal static GeneratorSettings Parse(string json)
        {
            // Adding external nuget dependencies to source generators is a bit troublesome,
            // therefore this class parses simple json using regexes (instead of Newtonsoft.Json or System.Text.Json).

            return new GeneratorSettings
            {
                FileHeader = GetStringValue(json, "fileHeader"),
                RootNamespace = GetStringValue(json, "rootNamespace"),
                TypesToGenerate = GetStringArrayValues(json, "typesToGenerate")
            };
        }

        private static string GetStringValue(string json, string key)
        {
            var regex = new Regex($@"""{key}""\s*:\s*""(?<value>[^""]*)""", RegexOptions.IgnoreCase);
            var match = regex.Match(json);

            return match?.Groups["value"]?.Value;
        }

        private static string[] GetStringArrayValues(string json, string key)
        {
            var regex = new Regex($@"""{key}""\s*:\s*\[(\s*""(?<value>[^""]*)""\s*,?\s*)*\]", RegexOptions.IgnoreCase);
            var match = regex.Match(json);

            return match?.Groups["value"]?.Captures.Cast<Capture>().Select(c => c.Value).ToArray();
        }
    }
}
