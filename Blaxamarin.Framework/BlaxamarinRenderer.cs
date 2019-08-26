using Emblazon;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public class BlaxamarinRenderer : EmblazonRenderer<Element>
    {
        public BlaxamarinRenderer(Microsoft.Extensions.DependencyInjection.ServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public ContentPage ContentPage { get; } = new ContentPage();

        protected override void InitializeRootAdapter(EmblazonAdapter<Element> adapter)
        {
            // TODO: Need to figure out a proper story for what the root adapter should be. Is it the Application? A ContentPage? A View? Etc.

            // TODO: Might actually want to keep this dummy control so that Blinforms can be an island in a form. But, need
            // to figure out its default size etc. Perhaps top-level Razor class implements ITopLevel{FormSettings} interface
            // to control 'container Form' options?
            //adapter.TargetControl = new Element()
            //{
            //    Dock = DockStyle.Fill,
            //};

        }

        protected override void HandleException(Exception exception)
        {
            Debug.Fail($"{nameof(HandleException)} called with '{exception.GetType().Name}': '{exception.Message}'");

            // TODO: Would be nice to dispatch this to some Xamarin.Forms-friendly error logic
            //MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override EmblazonAdapter<Element> CreateRootAdapter()
        {
            var rootContent = new ContentView();
            ContentPage.Content = rootContent;

            return CreateAdapter(rootContent);
        }

        protected override EmblazonAdapter<Element> CreateAdapter(Element physicalParent)
        {
            return new BlelementAdapter(physicalParent);
        }
    }
}
