// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
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
