using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public class BlinFormsRenderer : Renderer
    {
        private Dictionary<int, Blontrol> _componentIdToControl = new Dictionary<int, Blontrol>(); // TODO: Map to Control

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
            var control = _componentIdToControl[componentId] =
                new Blontrol(this)
                {
                    Size = new Size(500, 500),
                };
            RootForm.Controls.Add(control);
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
                var control = _componentIdToControl[updatedComponent.ComponentId];
                control.ApplyEdits(updatedComponent.Edits, renderBatch.ReferenceFrames);
            }

            return Task.CompletedTask;
        }

        internal Blontrol CreateControlForChildComponent(int componentId)
        {
            var result = new Blontrol(this);
            _componentIdToControl[componentId] = result;
            return result;
        }
    }
}
