using Xamarin.Forms;
using XamarinTest.WPF;

[assembly: Dependency(typeof(BaseUrl))]

namespace XamarinTest.WPF
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///";
        }
    }
}
