// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;

namespace BlinForms.Framework
{
    public class BlinFormsRootFormContent<TComponent> : IBlinFormsRootFormContent
        where TComponent : IComponent
    {
        public BlinFormsRootFormContent()
        {
            RootFormContentType = typeof(TComponent);
        }

        public Type RootFormContentType { get; }
    }
}
