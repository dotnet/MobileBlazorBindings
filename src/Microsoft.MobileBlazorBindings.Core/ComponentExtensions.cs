using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.MobileBlazorBindings.Core
{
    public static class ComponentExtensions
    {
        public static void SetNavigationParameters(this IComponent component, Dictionary<string, string> parameters)
        {
            if(component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            if(parameters == null || parameters.Count == 0)
            {
                //parameters will often be null. e.g. if you navigate with no parameters or when creating a root component.
                return;
            }

            foreach (var parameter in parameters)
            {
                var prop = component.GetType().GetProperty(parameter.Key);

                if (prop != null)
                {
                    var parameterAttribute = prop.GetCustomAttribute(typeof(ParameterAttribute));
                    if(parameterAttribute == null)
                    {
                        //I considered setting the property anyway even if it wasn't marked as a parameter but throwing exception is consistent with web Blazor
                        throw new InvalidOperationException($"Object of type '{component.GetType()}' has a property matching the name '{parameter.Key}', but it does not have [ParameterAttribute] or [CascadingParameterAttribute] applied.");
                    }

                    if(prop.PropertyType.TryParse(parameter.Value, out object result))
                    { 
                        prop.SetValue(component, result);
                    }
                    else
                    {
                        //Exception is consistent with web blazor
                        throw new InvalidCastException($"Unable to set property {parameter.Key} on object of type '{component.GetType()}' The error was: Specified cast is not valid. ");
                    }
                }
            }
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
        public static bool TryParse(this Type type, string s, out object result)
        {
            //On all happy paths we set the result to something usefule
            //This is here for if the value can not be parsed.
            result = null;

            //The if else ladder here is roughly in order of how often I think types are used for the shortest path possible.
            //e.g. string and ints are passed around all the time but I've never seen someone pass a float.
            if (type == typeof(string))
            {
                result = s;
                return true;
            }
            else if(type == typeof(int))
            {
                var success = int.TryParse(s, out int parsed);
                if (success)
                    result = parsed;
                return success;
            }
            else if(type == typeof(Guid))
            {
                var success = Guid.TryParse(s, out Guid parsed);
                if (success)
                    result = parsed;
                return success;
            }
            else if (type == typeof(bool))
            {
                var success = bool.TryParse(s, out bool parsed);
                if (success)
                    result = parsed;
                return success;
            }
            else if (type == typeof(DateTime))
            {
                var success = DateTime.TryParse(s, out DateTime parsed);
                if (success)
                    result = parsed;
                return success;
            }
            else if (type == typeof(decimal))
            {
                var success = decimal.TryParse(s, out decimal parsed);
                if (success)
                    result = parsed;
                return success;
            }
            //I'm bored, is there a better way?
            else if (type == typeof(double))
            {
                var success = double.TryParse(s, out double parsed);
                if (success)
                    result = parsed;
                return success;
            }
            else if (type == typeof(float))
            {
                var success = float.TryParse(s, out float parsed);
                if (success)
                    result = parsed;
                return success;
            }
            else if (type == typeof(long))
            {
                var success = long.TryParse(s, out long parsed);
                if (success)
                    result = parsed;
                return success;
            }

            return false;
        }

    }
}
