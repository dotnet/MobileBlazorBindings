using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ShellHandler : PageHandler
    {
        private readonly ShellContentMarkerItem _dummyShellContent = new ShellContentMarkerItem();

        public ShellHandler(EmblazonRenderer renderer, XF.Shell shellControl) : base(renderer, shellControl)
        {
            ShellControl = shellControl ?? throw new ArgumentNullException(nameof(shellControl));

            // Add one item for Shell to load correctly. It will later be removed when the first real
            // item is added by the app.
            ShellControl.Items.Add(_dummyShellContent);
            ShellControl.ChildAdded += OnShellControlChildAdded;

            ShellControl.Navigated += (s, e) =>
            {
                if (NavigatedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(NavigatedEventHandlerId, null, e));
                }
            };
            ShellControl.Navigating += (s, e) =>
            {
                if (NavigatingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(NavigatingEventHandlerId, null, e));
                }
            };
        }

        private void OnShellControlChildAdded(object sender, XF.ElementEventArgs e)
        {
            // Remove the dummy ShellContent if it's still there. This won't throw even if the item is already removed.
            ShellControl.Items.Remove(_dummyShellContent);
        }

        public XF.Shell ShellControl { get; }
        public ulong NavigatedEventHandlerId { get; set; }
        public ulong NavigatingEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                //[Parameter] public ShellItem CurrentItem { get; set; }
                //[Parameter] public ShellNavigationState CurrentState { get; }
                case nameof(XF.Shell.FlyoutBackgroundImage):
                    ShellControl.FlyoutBackgroundImage = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundImageAspect):
                    ShellControl.FlyoutBackgroundImageAspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundColor):
                    ShellControl.FlyoutBackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBehavior):
                    ShellControl.FlyoutBehavior = (XF.FlyoutBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                //object FlyoutHeader
                case nameof(XF.Shell.FlyoutHeaderBehavior):
                    ShellControl.FlyoutHeaderBehavior = (XF.FlyoutHeaderBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                //[Parameter] public DataTemplate FlyoutHeaderTemplate { get; set; }
                case nameof(XF.Shell.FlyoutIcon):
                    ShellControl.FlyoutIcon = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                //[Parameter] public bool? FlyoutIsPresented { get; set; } // TODO: Two-way binding?
                //[Parameter] public DataTemplate ItemTemplate { get; set; }
                //[Parameter] public DataTemplate MenuItemTemplate { get; set; }

                case "__ShellGoToState":
                    {
                        var shellGoToState = System.Text.Json.JsonSerializer.Deserialize<ShellGoToState>((string)attributeValue);
                        ShellControl.GoToAsync(new XF.ShellNavigationState(shellGoToState.Location), shellGoToState.Animate);
                    }
                    break;

                case "onnavigated":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => NavigatedEventHandlerId = 0);
                    NavigatedEventHandlerId = attributeEventHandlerId;
                    break;
                case "onnavigating":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => NavigatingEventHandlerId = 0);
                    NavigatingEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
