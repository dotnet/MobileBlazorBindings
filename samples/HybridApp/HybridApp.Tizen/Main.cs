using System;
using System.IO;
using System.Reflection;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.Tizen;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedRenderer))]

namespace HybridApp.Tizen
{
    class Program : FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            System.IO.Directory.SetCurrentDirectory(Current.DirectoryInfo.Resource);
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();

#if !TIZEN80
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                var asmName = e.Name.Split(",")[0];
                var dllPath = System.IO.Path.Combine(app.ApplicationInfo.ExecutablePath, "../", asmName + ".dll");
                return File.Exists(dllPath) ? Assembly.LoadFile(dllPath) : null;
            };
#endif

            BlazorHybridTizen.Init();
            Forms.Init(app, true);
            app.Run(args);
        }
    }
}
