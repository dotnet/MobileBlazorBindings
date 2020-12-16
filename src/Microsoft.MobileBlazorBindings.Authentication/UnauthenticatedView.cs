// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// A component that will handle an unauthenticated state in Mobile Blazor Bindings.
    /// </summary>
    public class UnauthenticatedView : ComponentBase
    {
        /// <summary>
        /// Gets or sets a <see cref="RenderFragment"/> with the UI to display while authentication is being started.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; } = DefaultChildFragment;

        /// <summary>
        /// The Authentication Service to use to sign in.
        /// </summary>
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// Tries to sign in on first render.
        /// </summary>
        /// <param name="firstRender">Whether this render is the first render.</param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await AuthenticationService.SignIn();
        }

        private static void DefaultChildFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "p");
            builder.AddContent(1, "Starting Secure Sign-in...");
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            base.BuildRenderTree(builder);
            builder.AddContent(0, ChildContent);
        }
    }
}
