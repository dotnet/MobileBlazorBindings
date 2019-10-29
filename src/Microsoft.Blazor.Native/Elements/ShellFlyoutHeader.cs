using Emblazon;
using Microsoft.Blazor.Native.Elements.Handlers;

namespace Microsoft.Blazor.Native.Elements
{
    // TODO: The base class for this type isn't quite right. It should be something more like View, because that's
    // what Shell.FlyoutHeader requires. But View doesn't have a public ctor and isn't useful on its own, so
    // ContentView is used, which has a settable Content property.
    internal class ShellFlyoutHeader : ContentView
    {
        static ShellFlyoutHeader()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellFlyoutHeader>(
                renderer => new ShellFlyoutHeaderHandler(renderer, new DummyElement()));
        }
    }
}
