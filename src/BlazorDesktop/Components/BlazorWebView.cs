using BlazorDesktop.Components.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using System;
using System.Collections.Generic;

namespace BlazorDesktop.Components
{
    public class BlazorWebView : View, IDisposable
    {
        static BlazorWebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<BlazorWebView>(renderer => new BlazorWebViewHandler(renderer, new Elements.BlazorWebView()));
        }

        internal static BlazorWebView FindById(long id) => _registry[id];

        static long _nextId;
        static readonly Dictionary<long, BlazorWebView> _registry = new Dictionary<long, BlazorWebView>();
        readonly long _thisId;

        public BlazorWebView()
        {
            lock(_registry)
            {
                _thisId = _nextId++;
                _registry.Add(_thisId, this);
            }
        }

        [Inject] internal IServiceProvider Services { get; private set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);
            builder.AddAttribute("BlazorWebViewId", _thisId);
        }

        void IDisposable.Dispose()
        {
            _registry.Remove(_thisId);
        }
    }
}
