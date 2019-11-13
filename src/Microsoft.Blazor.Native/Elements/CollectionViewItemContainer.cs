using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Blazor.Native.Elements.Handlers;

namespace Microsoft.Blazor.Native.Elements
{
    public class CollectionViewItemContainer : NativeControlComponentBase
    {
        static CollectionViewItemContainer()
        {
            ElementHandlerRegistry.RegisterElementHandler<CollectionViewItemContainer>(
                renderer => new CollectionViewItemContainerHandler(renderer, new DummyElement()));
        }

        [Parameter] public int DataIndex { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute(nameof(DataIndex), DataIndex);
        }

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
