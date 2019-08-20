using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel2 : SplitterPanelBase
    {
        static SplitterPanel2()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitterPanel2).FullName, new ComponentControlFactoryFunc<System.Windows.Forms.Control>((_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 2)));
        }
    }
}
