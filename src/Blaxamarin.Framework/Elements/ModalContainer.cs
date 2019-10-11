using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blaxamarin.Framework.Elements
{
    public class ModalContainer : NativeControlComponentBase
    {
        static ModalContainer()
        {
            // TODO: What type of element to create here? We probably need *something*, but not sure what. Maybe create a new dummy type.
            ElementHandlerRegistry.RegisterElementHandler<ModalContainer>(
                renderer => new ModalContainerHandler(renderer, new ModalContainerPlaceholderElement()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        private bool __ShowDialog { get; set; }
        private bool __DialogIsShown { get; set; }
        [Parameter] public EventCallback OnClosed { get; set; }


        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (__ShowDialog)
            {
                // TODO: Probably need to eventually have this contain various metadata (e.g. Modal vs. NonModal, animated, etc.)
                builder.AddAttribute(nameof(__ShowDialog), __ShowDialog);

                // Set this so that the child content dialog will render.
                __DialogIsShown = true;

                // Reset the property so that if ShowDialog is later called again, the render tree will have a diff
                // in it, causing the element handler to process the diff.
                __ShowDialog = false;
            }

            builder.AddAttribute("onclosed", OnClosed);

            //builder.AddAttribute("__onclosed", EventCallback.Factory.Create(this, OnClosed));
        }

        public void FinishClosingDialog()
        {
            __DialogIsShown = false;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (__ShowDialog || __DialogIsShown)
            {
                base.BuildRenderTree(builder);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        public void ShowDialog()
        {
            __ShowDialog = true;
            StateHasChanged();
        }
    }
}
