using Xamarin.Forms;
using XamarinTest.UWP;

[assembly: Dependency(typeof(BaseUrl))]

namespace XamarinTest.UWP
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///";
        }
    }
}
