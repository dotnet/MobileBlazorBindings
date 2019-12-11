// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    /// <summary>
    /// Placeholder element typically used with <see cref="ParentChildManager{TParent, TChild}"/>. This
    /// element is used when an instance of <see cref="XF.Element"/> is required, but is not used.
    /// </summary>
    public sealed class DummyElement : XF.Element
    {
        protected override void OnParentSet()
        {
            ThrowForInvalidOperation();
        }

        protected override void OnChildAdded(XF.Element child)
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
