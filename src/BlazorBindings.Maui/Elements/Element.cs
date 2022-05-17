// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace BlazorBindings.Maui.Elements
{
    public abstract class Element : NativeControlComponentBase
    {
        [Parameter] public string AutomationId { get; set; }
        [Parameter] public string ClassId { get; set; }
        [Parameter] public string StyleId { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalProperties { get; set; }

        public MC.Element NativeControl => (ElementHandler as Handlers.ElementHandler)?.ElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutomationId != null)
            {
                builder.AddAttribute(nameof(AutomationId), AutomationId);
            }
            if (ClassId != null)
            {
                builder.AddAttribute(nameof(ClassId), ClassId);
            }
            if (StyleId != null)
            {
                builder.AddAttribute(nameof(StyleId), StyleId);
            }
            if (AdditionalProperties != null)
            {
                foreach (var keyValue in AdditionalProperties)
                {
                    builder.AddAttribute(keyValue.Key, AttributeHelper.ObjectToAttribute(keyValue.Value));
                }
            }
        }
    }
}
