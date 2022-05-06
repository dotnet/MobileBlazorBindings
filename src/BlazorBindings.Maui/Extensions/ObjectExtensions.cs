// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Maui.Extensions
{
    internal static class ObjectExtensions
    {
        /// <summary>
        /// The only purpose of this method it to use it as delegate parameter.
        /// </summary>
        public static object This(this object @this) => @this;
    }
}
