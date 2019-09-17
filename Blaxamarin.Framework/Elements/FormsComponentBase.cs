using Emblazon;
using System;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public abstract class FormsComponentBase : NativeControlComponentBase
    {
#pragma warning disable IDE0060 // Remove unused parameter; will likely be used in the future
#pragma warning disable CA1801 // Parameter is never used; will likely be used in the future
        public static void ApplyAttribute(Element control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
#pragma warning restore CA1801 // Parameter is never used
#pragma warning restore IDE0060 // Remove unused parameter
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
