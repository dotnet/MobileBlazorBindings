// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.IO;
using System.Reflection;
using XF = Xamarin.Forms;
using XFS = Xamarin.Forms.StyleSheets;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class StyleSheetHandler : IXamarinFormsElementHandler
    {
        private XF.VisualElement _parentVisualElement;

        public StyleSheetHandler(NativeComponentRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public NativeComponentRenderer Renderer { get; }
        public XF.Element ElementControl => null;
        public object TargetElement => ElementControl;

        public Assembly Assembly { get; private set; }
        public string Resource { get; private set; }
        public string Text { get; private set; }

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(StyleSheet.Assembly):
                    Assembly = Assembly.Load((string)attributeValue);
                    UpdateParentStyleSheetIfPossible();
                    break;
                case nameof(StyleSheet.Resource):
                    Resource = (string)attributeValue;
                    UpdateParentStyleSheetIfPossible();
                    break;
                case nameof(StyleSheet.Text):
                    Text = (string)attributeValue;
                    UpdateParentStyleSheetIfPossible();
                    break;
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }

        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            throw new NotImplementedException();
        }

        public int GetPhysicalSiblingIndex()
        {
            return 0;
        }

        public bool IsParented()
        {
            return _parentVisualElement != null;
        }

        public bool IsParentedTo(XF.Element elementControl)
        {
            return _parentVisualElement == elementControl;
        }

        public void SetParent(XF.Element parent)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (!(parent is XF.VisualElement parentVisualElement))
            {
                throw new ArgumentNullException(nameof(parent), $"Expected parent to be of type '{typeof(XF.VisualElement).FullName}' but it is of type '{parent.GetType().FullName}'.");
            }
            _parentVisualElement = parentVisualElement;

            UpdateParentStyleSheetIfPossible();
        }

        private void UpdateParentStyleSheetIfPossible()
        {
            if (_parentVisualElement != null)
            {
                // TODO: Add logic to ensure same resource isn't added multiple times
                if (Resource != null)
                {
                    if (Assembly == null)
                    {
                        throw new InvalidOperationException($"Specifying a '{nameof(Resource)}' property value '{Resource}' requires also specifying the '{nameof(Assembly)}' property to indicate the assembly containing the resource.");
                    }
                    var styleSheet = XFS.StyleSheet.FromResource(resourcePath: Resource, assembly: Assembly);
                    _parentVisualElement.Resources.Add(styleSheet);
                }
                if (Text != null)
                {
                    using var reader = new StringReader(Text);
                    var styleSheet = XFS.StyleSheet.FromReader(reader);
                    _parentVisualElement.Resources.Add(styleSheet);
                }
            }
        }
    }
}
