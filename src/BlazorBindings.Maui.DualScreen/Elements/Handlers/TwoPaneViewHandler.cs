// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TwoPaneViewHandler : GridHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child is TwoPaneViewPane1View pane1View)
            {
                TwoPaneViewControl.Pane1 = pane1View;
            }
            else if (child is TwoPaneViewPane2View pane2View)
            {
                TwoPaneViewControl.Pane2 = pane2View;
            }
            else
            {
                throw new InvalidOperationException($"Unknown child type {child.GetType().FullName} being added to parent element type {GetType().FullName}.");
            }
        }

        public override void RemoveChild(XF.Element child)
        {
            if (child == TwoPaneViewControl.Pane1)
            {
                TwoPaneViewControl.Pane1 = null;
            }
            else if (child == TwoPaneViewControl.Pane2)
            {
                TwoPaneViewControl.Pane2 = null;
            }
        }
    }
}
