using Emblazon;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel2 : SplitterPanelBase
    {
        static SplitterPanel2()
        {
            NativeControlRegistry<IWindowsFormsControlHandler>.RegisterNativeControlComponent<SplitterPanel2>(
                (_, parentControl) => GetSplitterPanel(parentControl.Control, panelNumber: 2));
        }
    }
}
