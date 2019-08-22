using System.Globalization;

namespace Blaxamarin.Framework.Elements.AttributeHelpers
{
    /// <summary>
    /// Helper methods for components that need to serialize or deserialize <see cref="System.Double" /> objects.
    /// </summary>
    public static class DoubleAttributeHelper
    {
        public static string DoubleToString(double color)
        {
            return color.ToString("R", CultureInfo.InvariantCulture); // "R" --> Round-trip format specifier guarantees fidelity when parsing
        }

        public static double StringToDouble(string doubleString)
        {
            return double.Parse(doubleString, CultureInfo.InvariantCulture);
        }
    }
}
