// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class GradientBrushHandler : BrushHandler, IXamarinFormsContainerElementHandler
    {
        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (!(child is XF.GradientStop gradientStopChild))
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            if (physicalSiblingIndex <= GradientBrushControl.GradientStops.Count)
            {
                GradientBrushControl.GradientStops.Insert(physicalSiblingIndex, gradientStopChild);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but GradientBrushControl.GradientStops.Count={GradientBrushControl.GradientStops}");
                GradientBrushControl.GradientStops.Add(gradientStopChild);
            }
        }

        public int GetChildIndex(XF.Element child)
        {
            if (!(child is XF.GradientStop gradientStopChild))
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            return GradientBrushControl.GradientStops.IndexOf(gradientStopChild);
        }

        public void RemoveChild(XF.Element child)
        {
            if (!(child is XF.GradientStop gradientStopChild))
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            GradientBrushControl.GradientStops.Remove(gradientStopChild);
        }
    }
}