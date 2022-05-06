// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.IO;
using System.Reflection;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.StyleSheets;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class StyleSheetHandler : IMauiElementHandler, INonPhysicalChild
    {
        private MC.VisualElement _parentVisualElement;

        public StyleSheetHandler(NativeComponentRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public NativeComponentRenderer Renderer { get; }
        public MC.Element ElementControl => null;
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

        public bool IsParented()
        {
            return _parentVisualElement != null;
        }

        public bool IsParentedTo(MC.Element elementControl)
        {
            return _parentVisualElement == elementControl;
        }

        public void SetParent(MC.Element parent)
        {
            throw new NotImplementedException();
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
                    var styleSheet = MCS.StyleSheet.FromResource(resourcePath: Resource, assembly: Assembly);
                    _parentVisualElement.Resources.Add(styleSheet);
                }
                if (Text != null)
                {
                    using var reader = new StringReader(Text);
                    var styleSheet = MCS.StyleSheet.FromReader(reader);
                    _parentVisualElement.Resources.Add(styleSheet);
                }
            }
        }

        public void SetParent(object parentElement)
        {
            if (parentElement is null)
            {
                throw new ArgumentNullException(nameof(parentElement));
            }
            if (!(parentElement is MC.VisualElement parentVisualElement))
            {
                throw new ArgumentNullException(nameof(parentElement), $"Expected parent to be of type '{typeof(MC.VisualElement).FullName}' but it is of type '{parentElement.GetType().FullName}'.");
            }
            _parentVisualElement = parentVisualElement;

            UpdateParentStyleSheetIfPossible();
        }

        public void Remove()
        {
            throw new InvalidOperationException("Removing StyleSheet element is not supported.");
        }
    }
}
