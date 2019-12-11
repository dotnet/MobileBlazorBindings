// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Threading.Tasks;

namespace BlinForms.Framework
{
    /// <summary>
    /// Add an instance of this interface to the service collection for code to run when the
    /// app starts.
    /// </summary>
    public interface IBlinFormsStartup
    {
        Task OnStartAsync();
    }
}
