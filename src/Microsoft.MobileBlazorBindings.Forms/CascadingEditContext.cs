// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Microsoft.MobileBlazorBindings.Forms
{
    public class CascadingEditContext : ComponentBase
    {
        private EditContext _editContext;
        private bool _hasSetEditContextExplicitly;

        [Parameter]
        public EditContext EditContext
        {
            get => _editContext;
            set
            {
                _editContext = value;
                _hasSetEditContextExplicitly = value != null;
            }
        }

        [Parameter] public object Model { get; set; }

        [Parameter] public RenderFragment<EditContext> ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            if (_hasSetEditContextExplicitly && Model != null)
            {
                throw new InvalidOperationException($"{nameof(CascadingEditContext)} requires a {nameof(Model)} " +
                    $"parameter, or an {nameof(EditContext)} parameter, but not both.");
            }
            else if (!_hasSetEditContextExplicitly && Model == null)
            {
                throw new InvalidOperationException($"{nameof(CascadingEditContext)} requires either a {nameof(Model)} " +
                    $"parameter, or an {nameof(EditContext)} parameter, please provide one of these.");
            }

            // Update _editContext if we don't have one yet, or if they are supplying a
            // potentially new EditContext, or if they are supplying a different Model
            if (Model != null && Model != _editContext?.Model)
            {
                _editContext = new EditContext(Model!);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            // If _editContext changes, tear down and recreate all descendants.
            // This is so we can safely use the IsFixed optimization on CascadingValue,
            // optimizing for the common case where _editContext never changes.
            builder.OpenRegion(_editContext.GetHashCode());

            builder.OpenComponent<CascadingValue<EditContext>>(0);
            builder.AddAttribute(1, "IsFixed", true);
            builder.AddAttribute(2, "Value", _editContext);
            builder.AddAttribute(3, "ChildContent", ChildContent?.Invoke(_editContext));
            builder.CloseComponent();

            builder.CloseRegion();
        }
    }
}
