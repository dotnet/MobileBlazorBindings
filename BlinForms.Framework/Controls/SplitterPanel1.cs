namespace BlinForms.Framework.Controls
{
    public class SplitterPanel1 : SplitterPanelBase
    {
        static SplitterPanel1()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitterPanel1).FullName, new ComponentControlFactoryFunc((_, parentControl) => GetSplitterPanel(parentControl, panelNumber: 1)));
        }
    }
}
