using System.Windows.Forms;

namespace BlinForms.Framework
{
    public interface IComponentControlFactory
    {
        Control CreateControl(ComponentControlFactoryContext context);
    }
}
