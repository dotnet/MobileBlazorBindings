// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Reflection;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class StyleSheet : NativeControlComponentBase
    {
        static StyleSheet()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<StyleSheet>(renderer => new StyleSheetHandler(renderer));
        }

        [Parameter] public Assembly Assembly { get; set; }
        [Parameter] public string Resource { get; set; }
        [Parameter] public string Text { get; set; }

        // TODO: Consider adding properties for using the full set of StyleSheet factories:
        // - OBSOLETE: public static StyleSheet FromAssemblyResource(Assembly assembly, string resourceId, IXmlLineInfo lineInfo = null);
        // - public static StyleSheet FromResource(string resourcePath, Assembly assembly, IXmlLineInfo lineInfo = null);
        // - public static StyleSheet FromString(string stylesheet);
        // - public static StyleSheet FromReader(TextReader reader);

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Assembly != null)
            {
                builder.AddAttribute(nameof(Assembly), Assembly.FullName);
            }
            if (Resource != null)
            {
                builder.AddAttribute(nameof(Resource), Resource);
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
        }
    }
}
