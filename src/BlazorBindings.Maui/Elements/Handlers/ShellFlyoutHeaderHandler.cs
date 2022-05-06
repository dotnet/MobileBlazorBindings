// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ShellFlyoutHeaderHandler : IMauiContainerElementHandler, INonChildContainerElement
    {
        public ShellFlyoutHeaderHandler(NativeComponentRenderer renderer, DummyElement shellFlyoutHeaderDummyControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ShellFlyoutHeaderDummyControl = shellFlyoutHeaderDummyControl ?? throw new ArgumentNullException(nameof(shellFlyoutHeaderDummyControl));

            _parentChildManager = new ParentChildManager<MC.Shell, MC.View>(SetShellFlyoutHeader);
        }

        public NativeComponentRenderer Renderer { get; }
        public DummyElement ShellFlyoutHeaderDummyControl { get; }
        public MC.Element ElementControl => ShellFlyoutHeaderDummyControl;
        public object TargetElement => ElementControl;

        private readonly ParentChildManager<MC.Shell, MC.View> _parentChildManager;

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        public void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            _parentChildManager.SetChild(child);
        }

        public void RemoveChild(MC.Element child)
        {
            // TODO: This could probably be implemented at some point, but it isn't needed right now
            throw new NotImplementedException();
        }

        public int GetChildIndex(MC.Element child)
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

        public bool IsParentedTo(MC.Element parent)
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        public void SetParent(MC.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }

        private void SetShellFlyoutHeader(ParentChildManager<MC.Shell, MC.View> parentChildManager)
        {
            // See comment in ShellHandler..ctor. We can't re-set the FlyoutHeader itself, so we have
            // an intermediate ContentView and adjust its contents.
            var flyoutHeaderContentView = (MC.ContentView)parentChildManager.Parent.FlyoutHeader;
            flyoutHeaderContentView.IsVisible = true;
            flyoutHeaderContentView.Content = parentChildManager.Child;
        }

        public void SetParent(object parentElement)
        {
            _parentChildManager.SetParent((MC.Element)parentElement);
        }

        public void Remove()
        {
            var flyoutHeaderContentView = (MC.ContentView)_parentChildManager.Parent.FlyoutHeader;
            flyoutHeaderContentView.IsVisible = false;
            flyoutHeaderContentView.Content = null;

            _parentChildManager.SetParent(null);
        }
    }
}
