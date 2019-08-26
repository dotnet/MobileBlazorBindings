using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel1 : SplitterPanelBase
    {
        static SplitterPanel1()
        {
            NativeControlRegistry<System.Windows.Forms.Control>.RegisterNativeControlComponent<SplitterPanel1>(
                (_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 1));
        }
    }
}
