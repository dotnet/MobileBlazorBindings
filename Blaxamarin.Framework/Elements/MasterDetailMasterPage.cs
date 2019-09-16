using Emblazon;

namespace Blaxamarin.Framework.Elements
{
    internal class MasterDetailMasterPage : MasterDetailChildPageBase
    {
        static MasterDetailMasterPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<MasterDetailMasterPage, MasterPageWrapper>();
        }

        internal sealed class MasterPageWrapper : BlazorPageWrapper
        {
            public MasterPageWrapper()
            {
                // The Master page must have its Title set:
                // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
                Title = "Title";
            }
        }
    }
}