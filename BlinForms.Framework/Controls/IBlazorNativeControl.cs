using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    internal interface IBlazorNativeControl
    {
        void ApplyAttribute(ref RenderTreeFrame attribute);
    }
}
