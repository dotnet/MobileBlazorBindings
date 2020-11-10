// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Microsoft.MobileBlazorBindings.Core
{
    /// <summary>
    /// Utility intermediate class to make it easier to strongly-type a derived <see cref="ElementManager"/>.
    /// </summary>
    /// <typeparam name="TElementType"></typeparam>
    public abstract class ElementManager<TElementType> : ElementManager
    {
        private static TElementType ConvertToType(IElementHandler elementHandler, string parameterName)
        {
            if (!(elementHandler is TElementType))
            {
                throw new ArgumentException($"Expected parameter value of type '{elementHandler.GetType().FullName}' to be convertible to type '{typeof(TElementType).FullName}'.", parameterName);
            }
            return (TElementType)elementHandler;
        }

        public sealed override void AddChildElement(IElementHandler parentHandler, IElementHandler childHandler, int physicalSiblingIndex)
        {
            AddChildElement(ConvertToType(parentHandler, nameof(parentHandler)), ConvertToType(childHandler, nameof(childHandler)), physicalSiblingIndex);
        }

        public sealed override int GetPhysicalSiblingIndex(IElementHandler handler)
        {
            return GetPhysicalSiblingIndex(ConvertToType(handler, nameof(handler)));
        }

        public sealed override bool IsParented(IElementHandler handler)
        {
            return IsParented(ConvertToType(handler, nameof(handler)));
        }

        public sealed override bool IsParentOfChild(IElementHandler parentHandler, IElementHandler childHandler)
        {
            return IsParentOfChild(ConvertToType(parentHandler, nameof(parentHandler)), ConvertToType(childHandler, nameof(childHandler)));
        }

        public sealed override void RemoveChildElement(IElementHandler parentHandler, IElementHandler childHandler)
        {
            RemoveChildElement(ConvertToType(parentHandler, nameof(parentHandler)), ConvertToType(childHandler, nameof(childHandler)));
        }

        protected abstract void AddChildElement(TElementType elementType1, TElementType elementType2, int physicalSiblingIndex);
        protected abstract int GetPhysicalSiblingIndex(TElementType elementType);
        protected abstract bool IsParented(TElementType elementType);
        protected abstract bool IsParentOfChild(TElementType elementType1, TElementType elementType2);
        protected abstract void RemoveChildElement(TElementType elementType1, TElementType elementType2);
    }
}
