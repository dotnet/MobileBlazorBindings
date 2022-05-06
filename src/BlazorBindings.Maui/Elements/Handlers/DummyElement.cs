// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <summary>
    /// Placeholder element typically used with <see cref="ParentChildManager{TParent, TChild}"/>. This
    /// element is used when an instance of <see cref="MC.Element"/> is required, but is not used.
    /// </summary>
    public sealed class DummyElement : MC.Element
    {
        protected override void OnParentSet()
        {
            ThrowForInvalidOperation();
        }

        protected override void OnChildAdded(MC.Element child)
        {
            ThrowForInvalidOperation();
        }

        protected override void OnPropertyChanging(string propertyName)
        {
            ThrowForInvalidOperation();
        }

        private void ThrowForInvalidOperation()
        {
            throw new InvalidOperationException($"This operation is not supported because {GetType().FullName} should never be in an element tree.");
        }
    }
}
