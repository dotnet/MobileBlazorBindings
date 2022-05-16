// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.Elements;
using NUnit.Framework;

namespace BlazorBindings.UnitTests.Elements
{
    public class ElementTests
    {
        [Test]
        public void NativeControlShouldReturnNullWhenHandlerNotInitialized_Editor()
            => Assert.That(new Editor().NativeControl, Is.Null);

        [Test]
        public void NativeControlShouldReturnNullWhenHandlerNotInitialized_Label()
            => Assert.That(new Label().NativeControl, Is.Null);

        [Test]
        public void NativeControlShouldReturnNullWhenHandlerNotInitialized_CollectionView()
            => Assert.That(new CollectionView<string>().NativeControl, Is.Null);

        [Test]
        public void NativeControlShouldReturnNullWhenHandlerNotInitialized_ContentPage()
            => Assert.That(new ContentPage().NativeControl, Is.Null);

        [Test]
        public void NativeControlShouldReturnNullWhenHandlerNotInitialized_TapGestureRecognizer()
            => Assert.That(new TapGestureRecognizer().NativeControl, Is.Null);
    }
}
