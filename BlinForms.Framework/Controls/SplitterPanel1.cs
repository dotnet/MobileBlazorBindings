using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel1 : SplitterPanelBase
    {
        static SplitterPanel1()
        {
            NativeControlRegistry<IWindowsFormsControlHandler>.RegisterNativeControlComponent<SplitterPanel1>(
                (_, parentControl) => GetSplitterPanel(parentControl.Control, panelNumber: 1));
        }
    }
}
