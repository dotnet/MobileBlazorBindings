// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ShellFlyoutHeaderHandler : IXamarinFormsContainerElementHandler, INonChildContainerElement
    {
        public ShellFlyoutHeaderHandler(NativeComponentRenderer renderer, DummyElement shellFlyoutHeaderDummyControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ShellFlyoutHeaderDummyControl = shellFlyoutHeaderDummyControl ?? throw new ArgumentNullException(nameof(shellFlyoutHeaderDummyControl));

            _parentChildManager = new ParentChildManager<XF.Shell, XF.View>(SetShellFlyoutHeader);
        }

        public NativeComponentRenderer Renderer { get; }
        public DummyElement ShellFlyoutHeaderDummyControl { get; }
        public XF.Element ElementControl => ShellFlyoutHeaderDummyControl;
        public object TargetElement => ElementControl;

        private readonly ParentChildManager<XF.Shell, XF.View> _parentChildManager;

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            _parentChildManager.SetChild(child);
        }

        public void RemoveChild(XF.Element child)
        {
            // TODO: This could probably be implemented at some point, but it isn't needed right now
            throw new NotImplementedException();
        }

        public int GetChildIndex(XF.Element child)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return 0;
        }

        public bool IsParented()
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public bool IsParentedTo(XF.Element parent)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public void SetParent(XF.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }

        private void SetShellFlyoutHeader(ParentChildManager<XF.Shell, XF.View> parentChildManager)
        {
            // See comment in ShellHandler..ctor. We can't re-set the FlyoutHeader itself, so we have
            // an intermediate ContentView and adjust its contents.
            var flyoutHeaderContentView = (XF.ContentView)parentChildManager.Parent.FlyoutHeader;
            flyoutHeaderContentView.IsVisible = true;
            flyoutHeaderContentView.Content = parentChildManager.Child;
        }

        public void SetParent(object parentElement)
        {
            _parentChildManager.SetParent((XF.Element)parentElement);
        }
    }
}
