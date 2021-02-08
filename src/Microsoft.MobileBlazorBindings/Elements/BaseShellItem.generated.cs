// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class BaseShellItem : NavigableElement
    {
        static BaseShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<BaseShellItem>(
                renderer => new BaseShellItemHandler(renderer, new XF.BaseShellItem()));
        }

        /// <summary>
        /// The icon to use for the item. If this property is unset, it will fallback to using the <see cref="P:Xamarin.Forms.BaseShellItem.Icon" /> property value.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.ImageSource" /> that represents an icon.
        /// </value>
        [Parameter] public XF.ImageSource FlyoutIcon { get; set; }
        [Parameter] public bool? FlyoutItemIsVisible { get; set; }
        /// <summary>
        /// Defines the icon to display in parts of the chrome that are not the flyout.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.ImageSource" /> that represents an icon.
        /// </value>
        [Parameter] public XF.ImageSource Icon { get; set; }
        /// <summary>
        /// Defines if the item is selectable in the chrome.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the item is selectable in the chrome.
        /// </value>
        [Parameter] public bool? IsEnabled { get; set; }
        /// <summary>
        /// Indicates whether a FlyoutItem is included in tab navigation.
        /// </summary>
        /// <value>
        /// Default value is <see langword="true" />; when <see langword="false" />, the FlyoutItem is ignored by the tab-navigation infrastructure, irrespective if a TabIndex is set.
        /// </value>
        [Parameter] public bool? IsTabStop { get; set; }
        [Parameter] public bool? IsVisible { get; set; }
        /// <summary>
        /// The string used to address the item.
        /// </summary>
        /// <value>
        /// A unique string that identifies the item.
        /// </value>
        [Parameter] public string Route { get; set; }
        /// <summary>
        /// Indicates the order in which FlyoutItem objects receive focus when the user navigates through items by pressing the Tab key.
        /// </summary>
        /// <value>
        /// Defaults to 0.
        /// </value>
        [Parameter] public int? TabIndex { get; set; }
        /// <summary>
        /// Title to display in the UI.
        /// </summary>
        /// <value>
        /// Title to display in the UI.
        /// </value>
        [Parameter] public string Title { get; set; }

        public new XF.BaseShellItem NativeControl => ((BaseShellItemHandler)ElementHandler).BaseShellItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutIcon != null)
            {
                builder.AddAttribute(nameof(FlyoutIcon), AttributeHelper.ObjectToDelegate(FlyoutIcon));
            }
            if (FlyoutItemIsVisible != null)
            {
                builder.AddAttribute(nameof(FlyoutItemIsVisible), FlyoutItemIsVisible.Value);
            }
            if (Icon != null)
            {
                builder.AddAttribute(nameof(Icon), AttributeHelper.ObjectToDelegate(Icon));
            }
            if (IsEnabled != null)
            {
                builder.AddAttribute(nameof(IsEnabled), IsEnabled.Value);
            }
            if (IsTabStop != null)
            {
                builder.AddAttribute(nameof(IsTabStop), IsTabStop.Value);
            }
            if (IsVisible != null)
            {
                builder.AddAttribute(nameof(IsVisible), IsVisible.Value);
            }
            if (Route != null)
            {
                builder.AddAttribute(nameof(Route), Route);
            }
            if (TabIndex != null)
            {
                builder.AddAttribute(nameof(TabIndex), TabIndex.Value);
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
