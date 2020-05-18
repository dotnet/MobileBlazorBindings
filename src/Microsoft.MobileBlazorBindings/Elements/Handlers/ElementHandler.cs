// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ElementHandler : IXamarinFormsElementHandler
    {
        public ElementHandler(NativeComponentRenderer renderer, XF.Element elementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ElementControl = elementControl ?? throw new ArgumentNullException(nameof(elementControl));
        }

        protected void RegisterEvent(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            RegisteredEvents[eventName] = new EventRegistration(eventName, setId, clearId);
        }
        private Dictionary<string, EventRegistration> RegisteredEvents { get; } = new Dictionary<string, EventRegistration>();

        public NativeComponentRenderer Renderer { get; }
        public XF.Element ElementControl { get; }
        public object TargetElement => ElementControl;

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Element.AutomationId):
                    ElementControl.AutomationId = (string)attributeValue;
                    break;
                case nameof(XF.Element.ClassId):
                    ElementControl.ClassId = (string)attributeValue;
                    break;
                case nameof(XF.Element.StyleId):
                    ElementControl.StyleId = (string)attributeValue;
                    break;
                default:
                    if (!TryRegisterEvent(attributeName, attributeEventHandlerId))
                    {
                        throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
                    }
                    break;
            }
        }

        private bool TryRegisterEvent(string eventName, ulong eventHandlerId)
        {
            if (RegisteredEvents.TryGetValue(eventName, out var eventRegistration))
            {
                Renderer.RegisterEvent(eventHandlerId, eventRegistration.ClearId);
                eventRegistration.SetId(eventHandlerId);

                return true;
            }
            return false;
        }

        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{child.GetType().FullName}').");
        }

        public virtual int GetPhysicalSiblingIndex()
        {
            // TODO: What is the set of types that support child elements? Do they all need to be special-cased here? (Maybe...)
            var nativeComponent = ElementControl;

            if (nativeComponent is null)
            {
                // If there is no native object representing this element, bail out
                // TODO: Is this OK? It's probably OK for the same reason the GridCellHandler case above is OK. If this item has
                // no physical representation in the live UI tree, there's nothing to track.
                return 0;
            }

            if (nativeComponent.Parent is null)
            {
                // If this is the root element, the child's index is always 0
                return 0;
            }

            switch (nativeComponent.Parent)
            {
                case XF.Layout<XF.View> parentAsLayout:
                    {
                        var childAsView = nativeComponent as XF.View;
                        return parentAsLayout.Children.IndexOf(childAsView);
                    }
                case XF.ContentView _:
                    {
                        // A ContentView can have only 1 child, so the child's index is always 0.
                        return 0;
                    }
                case XF.ContentPage _:
                    {
                        // A ContentPage can have only 1 child, so the child's index is always 0.
                        return 0;
                    }
                case XF.TabbedPage parentAsTabbedPage:
                    {
                        var childAsPage = nativeComponent as XF.Page;
                        return parentAsTabbedPage.Children.IndexOf(childAsPage);
                    }
                case XF.ScrollView _:
                    {
                        // A ScrollView can have only 1 child, so the child's index is always 0.
                        return 0;
                    }
                case XF.Label parentAsLabel:
                    {
                        // There are two cases to consider:
                        // 1. A Xamarin.Forms Label can have only 1 child (a FormattedString), so the child's index is always 0.
                        // 2. But to simplify things, in MobileBlazorBindings a Label can contain a Span directly, so if the child
                        //    is a Span, we have to compute its sibling index.
                        if (nativeComponent is XF.Span childAsSpan)
                        {
                            return parentAsLabel.FormattedText?.Spans.IndexOf(childAsSpan) ?? 0;
                        }

                        return 0;
                    }
                case XF.FormattedString parentAsFormattedString:
                    {
                        var childAsSpan = nativeComponent as XF.Span;
                        return parentAsFormattedString.Spans.IndexOf(childAsSpan);
                    }
                case XF.ShellSection parentAsShellSection:
                    {
                        var childAsShellContent = nativeComponent as XF.ShellContent;
                        return parentAsShellSection.Items.IndexOf(childAsShellContent);
                    }
                case XF.ShellItem parentAsShellItem:
                    {
                        var childAsShellSection = nativeComponent as XF.ShellSection;
                        return parentAsShellItem.Items.IndexOf(childAsShellSection);
                    }
                case XF.Shell parentAsShell:
                    {
                        var childAsShellItem = nativeComponent as XF.ShellItem;
                        return parentAsShell.Items.IndexOf(childAsShellItem);
                    }
                case XF.Application _:
                    {
                        // An Application can have only 1 child (its MainPage), so the child's index is always 0.
                        return 0;
                    }
                default:
                    throw new InvalidOperationException($"Don't know how to handle parent element type {nativeComponent.Parent.GetType().FullName} in order to get index of sibling {nativeComponent.GetType().FullName}");
            }
        }

        public virtual bool IsParented()
        {
            return ElementControl.Parent != null;
        }

        public virtual bool IsParentedTo(XF.Element parent)
        {
            return ElementControl.Parent == parent;
        }

        public virtual void SetParent(XF.Element parent)
        {
            ElementControl.Parent = parent;
        }
    }
}
