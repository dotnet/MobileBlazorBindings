using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    internal interface IBlazorNativeControl
    {
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
    }
}
