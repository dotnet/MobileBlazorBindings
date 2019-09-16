using Emblazon;

namespace Blaxamarin.Framework.Elements
{
    internal class MasterDetailDetailPage : MasterDetailChildPageBase
    {
        static MasterDetailDetailPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<MasterDetailDetailPage, DetailPageWrapper>();
        }

        internal sealed class DetailPageWrapper : BlazorPageWrapper { }
    }
}