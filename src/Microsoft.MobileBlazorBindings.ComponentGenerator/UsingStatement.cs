﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.ComponentGenerator
{
    internal sealed class UsingStatement
    {
        public string Alias { get; set; }
        public string Namespace { get; set; }
        public bool IsUsed { get; set; }

        public string ComparableString => Alias?.ToUpperInvariant() ?? Namespace?.ToUpperInvariant();

        public string UsingText => $"using {(Alias != null ? Alias + " = " : "")}{Namespace};";
    }
}
