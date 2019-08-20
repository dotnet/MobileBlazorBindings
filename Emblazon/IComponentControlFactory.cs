namespace Emblazon
{
    public interface IComponentControlFactory<TNativeComponent> where TNativeComponent : class
    {
        TNativeComponent CreateControl(ComponentControlFactoryContext<TNativeComponent> context);
    }
}
