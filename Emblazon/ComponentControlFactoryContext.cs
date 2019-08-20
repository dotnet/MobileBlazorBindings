namespace Emblazon
{
    public class ComponentControlFactoryContext<TNativeComponent> where TNativeComponent : class
    {
        public ComponentControlFactoryContext(EmblazonRenderer<TNativeComponent> renderer, TNativeComponent parentControl)
        {
            Renderer = renderer ?? throw new System.ArgumentNullException(nameof(renderer));
            ParentControl = parentControl;
        }

        public TNativeComponent ParentControl { get; }

        public EmblazonRenderer<TNativeComponent> Renderer { get; }
    }
}
