// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class CheckBox : View
    {
        static CheckBox()
        {
            ElementHandlerRegistry.RegisterElementHandler<CheckBox>(
                renderer => new CheckBoxHandler(renderer, new XF.CheckBox()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public XF.Color? Color { get; set; }
        [Parameter] public bool? IsChecked { get; set; }

        public new XF.CheckBox NativeControl => ((CheckBoxHandler)ElementHandler).CheckBoxControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }
            if (IsChecked != null)
            {
                builder.AddAttribute(nameof(IsChecked), IsChecked.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
