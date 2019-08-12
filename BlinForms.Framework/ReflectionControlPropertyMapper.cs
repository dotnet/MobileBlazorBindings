using BlinForms.Framework.Controls;
using System.Diagnostics;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    internal class ReflectionControlPropertyMapper : IControlPropertyMapper
    {
        private readonly Control _control;

        public ReflectionControlPropertyMapper(Control control)
        {
            _control = control;
        }

        public void SetControlProperty(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (_control is IBlazorNativeControl nativeControl)
            {
                // TODO: Does this need to return a 'bool' so we know if it actually set the property?
                nativeControl.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                return;
            }

            // TODO: Figure out how to wire up events, e.g. OnClick
            var propertyInfo = _control.GetType().GetProperty(attributeName);
            if (propertyInfo != null)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(_control, attributeValue);
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(_control, int.Parse((string)attributeValue));
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo.SetValue(_control, attributeValue);
                }
                else
                {
                    Debug.WriteLine($"Unknown property type '{propertyInfo.PropertyType}' for '{attributeName}' on '{_control.GetType().FullName}'.");
                }
            }
            else
            {
                Debug.WriteLine($"Unknown property '{attributeName}' on '{_control.GetType().FullName}'. Maybe an event handler?");
            }
        }
    }
}
