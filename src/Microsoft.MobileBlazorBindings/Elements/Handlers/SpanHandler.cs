// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SpanHandler : GestureElementHandler, IHandleChildContentText
    {
        private readonly TextSpanContainer _textSpanContainer = new TextSpanContainer(trimWhitespace: false);

        public void HandleText(int index, string text)
        {
            SpanControl.Text = _textSpanContainer.GetUpdatedText(index, text);
        }
    }
}
