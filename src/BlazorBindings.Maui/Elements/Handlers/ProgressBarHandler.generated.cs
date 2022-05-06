// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ProgressBarHandler : ViewHandler
    {
        private static readonly double ProgressDefaultValue = MC.ProgressBar.ProgressProperty.DefaultValue is double value ? value : default;
        private static readonly Color ProgressColorDefaultValue = MC.ProgressBar.ProgressColorProperty.DefaultValue is Color value ? value : default;

        public ProgressBarHandler(NativeComponentRenderer renderer, MC.ProgressBar progressBarControl) : base(renderer, progressBarControl)
        {
            ProgressBarControl = progressBarControl ?? throw new ArgumentNullException(nameof(progressBarControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ProgressBar ProgressBarControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ProgressBar.Progress):
                    ProgressBarControl.Progress = AttributeHelper.StringToDouble((string)attributeValue, ProgressDefaultValue);
                    break;
                case nameof(MC.ProgressBar.ProgressColor):
                    ProgressBarControl.ProgressColor = AttributeHelper.StringToColor((string)attributeValue, ProgressColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
