// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings
{
    public static class AttachedPropertyRegistry
    {
        internal static readonly Dictionary<string, Action<Element, object>> AttachedPropertyHandlers = new();

        public static void RegisterAttachedPropertyHandler(string propertyName, Action<Element, object> handler)
        {
            AttachedPropertyHandlers[propertyName] = handler;
        }
    }
}
