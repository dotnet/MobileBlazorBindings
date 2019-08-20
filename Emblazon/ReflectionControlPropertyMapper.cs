using System.Diagnostics;

namespace Emblazon
{
    internal class ReflectionControlPropertyMapper : IControlPropertyMapper
    {
        private readonly object _component;

        public ReflectionControlPropertyMapper(object component)
        {
            _component = component;
        }

        public void SetControlProperty(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            // TODO: Figure out how to wire up events, e.g. OnClick
            var propertyInfo = _component.GetType().GetProperty(attributeName);
            if (propertyInfo != null)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(_component, attributeValue);
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(_component, int.Parse((string)attributeValue));
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo.SetValue(_component, attributeValue);
                }
                else
                {
                    Debug.WriteLine($"Unknown property type '{propertyInfo.PropertyType}' for '{attributeName}' on '{_component.GetType().FullName}'.");
                }
            }
            else
            {
                Debug.WriteLine($"Unknown property '{attributeName}' on '{_component.GetType().FullName}'. Maybe an event handler?");
            }
        }
    }
}
