// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace MobileBlazorBindingsHelloWorld.Tizen
{
    class Program : FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app, true);
            app.Run(args);
        }
    }
}
