namespace Emblazon
{
    internal class ComponentControlFactoryContext<TControlHandler> where TControlHandler : class, INativeControlHandler
    {
        public ComponentControlFactoryContext(EmblazonRenderer<TControlHandler> renderer, TControlHandler parentHandler)
        {
            Renderer = renderer ?? throw new System.ArgumentNullException(nameof(renderer));
            ParentHandler = parentHandler;
        }

        public TControlHandler ParentHandler { get; }

        public EmblazonRenderer<TControlHandler> Renderer { get; }
    }
}
