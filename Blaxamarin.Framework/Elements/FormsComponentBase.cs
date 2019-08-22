using Emblazon;
using System;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public abstract class FormsComponentBase : NativeControlComponentBase
    {
        public static void ApplyAttribute(Element control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
