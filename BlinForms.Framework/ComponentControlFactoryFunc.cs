using System;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class ComponentControlFactoryFunc : IComponentControlFactory
    {
        private readonly Func<BlinFormsRenderer, Control, Control> _callback;

        public ComponentControlFactoryFunc(Func<BlinFormsRenderer, Control, Control> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public Control CreateControl(ComponentControlFactoryContext context)
        {
            return _callback(context.Renderer, context.ParentControl);
        }
    }
}
