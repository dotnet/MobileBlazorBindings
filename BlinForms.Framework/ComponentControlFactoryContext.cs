using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class ComponentControlFactoryContext
    {
        public ComponentControlFactoryContext(BlinFormsRenderer renderer, Control parentControl)
        {
            Renderer = renderer ?? throw new System.ArgumentNullException(nameof(renderer));
            ParentControl = parentControl;
        }

        public Control ParentControl { get; }
        public BlinFormsRenderer Renderer { get; }
    }
}
