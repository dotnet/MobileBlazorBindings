// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public interface IWindowsFormsControlHandler : IElementHandler
    {
        Control Control { get; }
    }
}
