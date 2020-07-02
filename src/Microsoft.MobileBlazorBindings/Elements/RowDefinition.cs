// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.GridInternals;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class RowDefinition : NativeControlComponentBase
    {
        [CascadingParameter] private GridMetadata GridMetadata { get; set; }

        [Parameter]
        public double? Height
        {
            get { return _rowDefinition.Height; }
            set { _rowDefinition.Height = value.Value; }
        }

        [Parameter]
        public XF.GridUnitType? GridUnitType
        {
            get { return _rowDefinition.GridUnitType; }
            set { _rowDefinition.GridUnitType = value.Value; }
        }

        private readonly RowDefinitionMetadata _rowDefinition = new RowDefinitionMetadata();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // This component doesn't render anything; it forwards all its data to its container (Grid)
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                // On first render, add this component's metadata to its container. After this, all
                // subsequent changes will be done via change notifications on the same instances.
                GridMetadata.RowDefinitions.Add(_rowDefinition);
            }
        }
    }
}
