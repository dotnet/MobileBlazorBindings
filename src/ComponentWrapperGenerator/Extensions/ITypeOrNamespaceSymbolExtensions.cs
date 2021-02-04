// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace ComponentWrapperGenerator.Extensions
{
    internal static class ITypeOrNamespaceSymbolExtensions
    {
        internal static string GetFullName(this INamespaceOrTypeSymbol namespaceOrType)
        {
            var stack = new Stack<string>();

            stack.Push(GetName(namespaceOrType));

            if (namespaceOrType.ContainingType != null)
            {
                stack.Push(GetName(namespaceOrType.ContainingType));
            }

            var currentNamespace = namespaceOrType.ContainingNamespace;
            while (!currentNamespace.IsGlobalNamespace)
            {
                stack.Push(currentNamespace.Name);
                currentNamespace = currentNamespace.ContainingNamespace;
            }

            return string.Join('.', stack);
        }

        /// <summary>
        /// Returns name with generic type arguments (is any).
        /// </summary>
        private static string GetName(INamespaceOrTypeSymbol namespaceOrType)
        {
            if (namespaceOrType is INamedTypeSymbol namedType && namedType.IsGenericType)
            {
                var genericTypesNames = string.Join(", ", namedType.TypeArguments.Select(GetFullName));
                return $"{namedType.Name}<{genericTypesNames}>";
            }
            else
            {
                return namespaceOrType.Name;
            }
        }
    }
}
