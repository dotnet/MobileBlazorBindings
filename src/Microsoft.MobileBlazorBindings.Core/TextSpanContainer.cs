// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings.Core
{
    /// <summary>
    /// Helper class for types that accept inline text spans. This type collects text spans
    /// and returns the string represented by the contained text spans.
    /// </summary>
    public class TextSpanContainer
    {
        private readonly List<string> _textSpans = new List<string>();

        public TextSpanContainer(bool trimWhitespace = true)
        {
            TrimWhitespace = trimWhitespace;
        }

        public bool TrimWhitespace { get; }

        /// <summary>
        /// Updates the text spans with the new text at the new index and returns the new
        /// string represented by the contained text spans.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetUpdatedText(int index, string text)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index >= _textSpans.Count)
            {
                // Expand the list to allow for the new text's index to exist
                _textSpans.AddRange(new string[index - _textSpans.Count + 1]);
            }
            _textSpans[index] = text;

            var allText = string.Join(string.Empty, _textSpans);
            return TrimWhitespace
                ? allText?.Trim()
                : allText;
        }
    }
}
