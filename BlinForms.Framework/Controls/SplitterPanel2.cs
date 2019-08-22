using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel2 : SplitterPanelBase
    {
        static SplitterPanel2()
        {
            BlontrolAdapter.RegisterNativeControlComponent<SplitterPanel2>(
                (_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 2));
        }
    }
}
