using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class BlinFormsRenderer : Renderer
    {
        private readonly Dictionary<int, BlontrolAdapter> _componentIdToAdapter = new Dictionary<int, BlontrolAdapter>();

        public BlinFormsRenderer(IServiceProvider serviceProvider)
            : base(serviceProvider, new LoggerFactory())
        {
        }

        public Form RootForm { get; private set; } = new RootForm();

        public override Dispatcher Dispatcher { get; }
             = Dispatcher.CreateDefault();

        public Task AddComponent<T>() where T : IComponent
        {
            var component = InstantiateComponent(typeof(T));
            var componentId = AssignRootComponentId(component);
            var adapter = _componentIdToAdapter[componentId] =
                new BlontrolAdapter(this)
                {
                    Name = "Root BlontrolAdapter",
                    // TODO: Might actually want to keep this dummy control so that Blinforms can be an island in a form. But, need
                    // to figure out its default size etc. Perhaps top-level Razor class implements ITopLevel{FormSettings} interface
                    // to control 'container Form' options?
                    TargetControl = new Control()
                    {
                        Dock = DockStyle.Fill,
                    },
                };
            RootForm.Controls.Add(adapter.TargetControl);
            return RenderRootComponentAsync(componentId);
        }

        protected override void HandleException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            foreach (var updatedComponent in renderBatch.UpdatedComponents.Array.Take(renderBatch.UpdatedComponents.Count))
            {
                var adapter = _componentIdToAdapter[updatedComponent.ComponentId];
                adapter.ApplyEdits(updatedComponent.ComponentId, updatedComponent.Edits, renderBatch.ReferenceFrames, renderBatch);
            }

            return Task.CompletedTask;
        }

        internal BlontrolAdapter CreateAdapterForChildComponent(int componentId)
        {
            var result = new BlontrolAdapter(this);
            _componentIdToAdapter[componentId] = result;
            return result;
        }
    }
}
