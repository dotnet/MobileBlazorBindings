using Emblazon;

namespace Microsoft.Blazor.Native.Elements
{
    public class NavigableElement : Element
    {
        private bool __CloseDialog { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (__CloseDialog)
            {
                // TODO: Probably need to eventually have this contain various metadata (e.g. Modal vs. NonModal, animated, etc.)
                builder.AddAttribute(nameof(__CloseDialog), __CloseDialog);

                // Reset the property so that if ShowDialog is later called again, the render tree will have a diff
                // in it, causing the element handler to process the diff.
                __CloseDialog = false;
            }
        }

        public void CloseDialog()
        {
            __CloseDialog = true;
            StateHasChanged();
        }
    }
}
