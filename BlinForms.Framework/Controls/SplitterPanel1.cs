using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel1 : SplitterPanelBase
    {
        static SplitterPanel1()
        {
            BlontrolAdapter.RegisterNativeControlComponent<SplitterPanel1>(
                (_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 1));
        }
    }
}
