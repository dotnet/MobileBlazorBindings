using Emblazon;
using System.Linq;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live Xamarin.Forms Element tree.
    /// </summary>
    public class BlelementAdapter : EmblazonAdapter<Element>
    {
        public BlelementAdapter(Element physicalParent) : base(physicalParent)
        {
        }

        protected override EmblazonAdapter<Element> CreateAdapter(Element physicalParent)
        {
            return new BlelementAdapter(physicalParent);
        }
    }
}
