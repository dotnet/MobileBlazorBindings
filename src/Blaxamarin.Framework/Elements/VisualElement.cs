using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class VisualElement : NavigableElement
    {
        [Parameter] public XF.Color? BackgroundColor { get; set; }
        [Parameter] public double? HeightRequest { get; set; }
        //[Parameter] public bool? InputTransparent { get; set; }
        [Parameter] public bool? IsEnabled { get; set; }
        [Parameter] public bool? IsTabStop { get; set; }
        [Parameter] public bool? IsVisible { get; set; }
        //[Parameter] public double? MinimumHeightRequest { get; set; }
        //[Parameter] public double? MinimumWidthRequest { get; set; }
        //[Parameter] public double? Opacity { get; set; }
        //[Parameter] public double? Rotation { get; set; }
        //[Parameter] public double? RotationX { get; set; }
        //[Parameter] public double? RotationY { get; set; }
        //[Parameter] public double? Scale { get; set; }
        //[Parameter] public double? ScaleX { get; set; }
        //[Parameter] public double? ScaleY { get; set; }
        //[Parameter] public int? TabIndex { get; set; }
        //[Parameter] public double? TranslationX { get; set; }
        //[Parameter] public double? TranslationY { get; set; }
        [Parameter] public double? WidthRequest { get; set; }

        [Parameter] public EventCallback<XF.FocusEventArgs> OnFocused { get; set; }
        [Parameter] public EventCallback OnSizeChanged { get; set; }
        [Parameter] public EventCallback<XF.FocusEventArgs> OnUnfocused { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundColor != null)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor.Value));
            }
            if (HeightRequest != null)
            {
                builder.AddAttribute(nameof(HeightRequest), AttributeHelper.DoubleToString(HeightRequest.Value));
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
            if (WidthRequest != null)
            {
                builder.AddAttribute(nameof(WidthRequest), AttributeHelper.DoubleToString(WidthRequest.Value));
            }

            builder.AddAttribute("onfocused", OnFocused);
            builder.AddAttribute("onsizechanged", OnSizeChanged);
            builder.AddAttribute("onunfocused", OnUnfocused);
        }
    }
}
