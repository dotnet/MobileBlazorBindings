using Emblazon;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live WinForms control tree.
    /// </summary>
    public class BlontrolAdapter : EmblazonAdapter<Control>
    {
        // Notice how all the methods here could be static (none of them operate on any members
        // of BlontrolAdapter). This suggests that EmblazonAdapter should instead be a private
        // implementation detail of Emblazon, not something subclassed for each UI tech. Instead,
        // each UI tech should provide a singleton-like implementation of some interface that
        // contains these methods. That service only needs to know how to update the physical tree
        // given parent/child/index params and doesn't need any concept of adapters.

        public BlontrolAdapter(Control physicalParent) : base(physicalParent)
        {
        }

        protected override EmblazonAdapter<Control> CreateAdapter(Control physicalParent)
        {
            return new BlontrolAdapter(physicalParent);
        }
    }
}
