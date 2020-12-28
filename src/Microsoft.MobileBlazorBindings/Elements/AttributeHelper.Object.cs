using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        public static AttributeValueHolder ObjectToDelegate(object item)
        {
            if(item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            return AttributeValueHolderFactory.FromObject(item);
        }

        public static object DelegateToObject(object item, object defaultValueIfNull = default)
        {
            return AttributeValueHolderFactory.ToValue(item, defaultValueIfNull);
        }
    }
}
