using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel2 : SplitterPanelBase
    {
        static SplitterPanel2()
        {
            NativeControlRegistry<System.Windows.Forms.Control>.RegisterNativeControlComponent<SplitterPanel2>(
                (_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 2));
        }
    }
}
