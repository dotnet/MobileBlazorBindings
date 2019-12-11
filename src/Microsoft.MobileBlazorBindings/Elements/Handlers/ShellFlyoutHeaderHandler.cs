// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ShellFlyoutHeaderHandler : IXamarinFormsElementHandler, IParentChildManagementRequired
    {
        public ShellFlyoutHeaderHandler(NativeComponentRenderer renderer, DummyElement shellFlyoutHeaderDummyControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ShellFlyoutHeaderDummyControl = shellFlyoutHeaderDummyControl ?? throw new ArgumentNullException(nameof(shellFlyoutHeaderDummyControl));

            ParentChildManager = new ParentChildManager<XF.Shell, XF.View>(SetShellFlyoutHeader);
        }

        public NativeComponentRenderer Renderer { get; }
        public DummyElement ShellFlyoutHeaderDummyControl { get; }
        public XF.Element ElementControl => ShellFlyoutHeaderDummyControl;
        public object TargetElement => ElementControl;

        public IParentChildManager ParentChildManager { get; }

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        private void SetShellFlyoutHeader(ParentChildManager<XF.Shell, XF.View> parentChildManager)
        {
            // See comment in ShellHandler..ctor. We can't re-set the FlyoutHeader itself, so we have
            // an intermediate ContentView and adjust its contents.
            var flyoutHeaderContentView = (XF.ContentView)parentChildManager.Parent.FlyoutHeader;
            flyoutHeaderContentView.IsVisible = true;
            flyoutHeaderContentView.Content = parentChildManager.Child;
        }
    }
}
