using Emblazon;

namespace Blaxamarin.Framework.Elements
{
    internal class MasterDetailDetailPage : MasterDetailChildPageBase
    {
        static MasterDetailDetailPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<MasterDetailDetailPage, DetailPageHandler>();
        }

        protected sealed class DetailPageHandler : ContentPageHandler { }
    }
}