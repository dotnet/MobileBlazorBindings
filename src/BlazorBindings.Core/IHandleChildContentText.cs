// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core
{
    /// <summary>
    /// Defines a mechanism for an <see cref="IElementHandler"/> to accept inline text.
    /// </summary>
    public interface IHandleChildContentText
    {
        /// <summary>
        /// This method is called to process inline text found in a component.
        /// </summary>
        /// <param name="index">the index of the string within a group of text strings.</param>
        /// <param name="text">The text to handle. This text may contain whitespace at the start and end of the string.</param>
        void HandleText(int index, string text);
    }
}
