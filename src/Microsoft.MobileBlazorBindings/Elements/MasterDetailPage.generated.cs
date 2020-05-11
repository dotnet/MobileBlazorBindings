// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class MasterDetailPage : Page
    {
        static MasterDetailPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<MasterDetailPage>(
                renderer => new MasterDetailPageHandler(renderer, new XF.MasterDetailPage()));
        }

        /// <summary>
        /// Gets or sets a value that turns on or off the gesture to reveal the master page. This is a bindable property.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if gesture is enabled; otherwise <see langword="false" />. Default is <see langword="true" />.
        /// </value>
        [Parameter] public bool? IsGestureEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates whether or not the visual element that is represented by the <see cref="P:Xamarin.Forms.MasterDetailPage.Master" /> property is presented to the user.
        /// </summary>
        [Parameter] public bool? IsPresented { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates how detail content is displayed.
        /// </summary>
        [Parameter] public XF.MasterBehavior? MasterBehavior { get; set; }

        public new XF.MasterDetailPage NativeControl => ((MasterDetailPageHandler)ElementHandler).MasterDetailPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsGestureEnabled != null)
            {
                builder.AddAttribute(nameof(IsGestureEnabled), IsGestureEnabled.Value);
            }
            if (IsPresented != null)
            {
                builder.AddAttribute(nameof(IsPresented), IsPresented.Value);
            }
            if (MasterBehavior != null)
            {
                builder.AddAttribute(nameof(MasterBehavior), (int)MasterBehavior.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
