namespace BlinForms.Framework.Controls
{
    public class SplitterPanel2 : SplitterPanelBase
    {
        static SplitterPanel2()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitterPanel2).FullName, new ComponentControlFactoryFunc((_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 2)));
        }
    }
}
