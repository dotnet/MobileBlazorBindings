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

        protected override NativeControlManager<Element> CreateNativeControlManager()
        {
            return new BlaxamarinNativeControlManager();
        }
    }
}
