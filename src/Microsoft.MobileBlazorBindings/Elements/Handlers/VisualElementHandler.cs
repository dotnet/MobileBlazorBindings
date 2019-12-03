using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class VisualElementHandler : NavigableElementHandler
    {
        public VisualElementHandler(EmblazonRenderer renderer, XF.VisualElement visualElementControl) : base(renderer, visualElementControl)
        {
            VisualElementControl = visualElementControl ?? throw new ArgumentNullException(nameof(visualElementControl));
            VisualElementControl.Focused += (s, e) =>
            {
                if (FocusedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(FocusedEventHandlerId, null, e));
                }
            };
            VisualElementControl.SizeChanged += (s, e) =>
            {
                if (SizeChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SizeChangedEventHandlerId, null, e));
                }
            };
            VisualElementControl.Unfocused += (s, e) =>
            {
                if (UnfocusedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(UnfocusedEventHandlerId, null, e));
                }
            };
        }

        public XF.VisualElement VisualElementControl { get; }
        public ulong FocusedEventHandlerId { get; set; }
        public ulong SizeChangedEventHandlerId { get; set; }
        public ulong UnfocusedEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.VisualElement.AnchorX):
                    VisualElementControl.AnchorX = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.AnchorY):
                    VisualElementControl.AnchorY = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.BackgroundColor):
                    VisualElementControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.HeightRequest):
                    VisualElementControl.HeightRequest = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.InputTransparent):
                    VisualElementControl.InputTransparent = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.VisualElement.IsEnabled):
                    VisualElementControl.IsEnabled = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.VisualElement.IsTabStop):
                    VisualElementControl.IsTabStop = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.VisualElement.IsVisible):
                    VisualElementControl.IsVisible = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.VisualElement.MinimumHeightRequest):
                    VisualElementControl.MinimumHeightRequest = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.MinimumWidthRequest):
                    VisualElementControl.MinimumWidthRequest = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.Opacity):
                    VisualElementControl.Opacity = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.Rotation):
                    VisualElementControl.Rotation = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.RotationX):
                    VisualElementControl.RotationX = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.RotationY):
                    VisualElementControl.RotationY = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.Scale):
                    VisualElementControl.Scale = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.ScaleX):
                    VisualElementControl.ScaleX = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.ScaleY):
                    VisualElementControl.ScaleY = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.TabIndex):
                    VisualElementControl.TabIndex = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.VisualElement.TranslationX):
                    VisualElementControl.TranslationX = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.TranslationY):
                    VisualElementControl.TranslationY = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.WidthRequest):
                    VisualElementControl.WidthRequest = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case "onfocused":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => FocusedEventHandlerId = 0);
                    FocusedEventHandlerId = attributeEventHandlerId;
                    break;
                case "onsizechanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => SizeChangedEventHandlerId = 0);
                    SizeChangedEventHandlerId = attributeEventHandlerId;
                    break;
                case "onunfocused":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => UnfocusedEventHandlerId = 0);
                    UnfocusedEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
