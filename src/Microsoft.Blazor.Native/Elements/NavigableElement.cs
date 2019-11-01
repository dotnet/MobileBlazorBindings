using Microsoft.Blazor.Native.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.Blazor.Native.Elements
{
    public class NavigableElement : Element
    {
        public async Task PopModalAsync(bool animated = true)
        {
            await ((NavigableElementHandler)ElementHandler).NavigableElementControl.Navigation.PopModalAsync(animated).ConfigureAwait(true);
        }
    }
}
