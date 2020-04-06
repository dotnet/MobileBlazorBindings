using System;
using System.Collections.Generic;
using System.IO;

namespace WebWindows.Blazor
{
    public class WebWindow
    {
        private string windowTitle;

        public Action<object, string> OnWebMessageReceived { get; internal set; }

        public WebWindow(string windowTitle, Action<WebWindowOptions> p)
        {
            this.windowTitle = windowTitle;
        }

        internal void NavigateToUrl(string v)
        {
            throw new NotImplementedException();
        }

        internal void WaitForExit()
        {
            throw new NotImplementedException();
        }

        internal void ShowMessage(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        internal void Invoke(Action p)
        {
            throw new NotImplementedException();
        }

        internal void SendMessage(string v)
        {
            throw new NotImplementedException();
        }
    }

    public class WebWindowOptions
    {
        public WebWindow Parent { get; set; }

        public IDictionary<string, ResolveWebResourceDelegate> SchemeHandlers { get; }
            = new Dictionary<string, ResolveWebResourceDelegate>();
    }

    public delegate Stream ResolveWebResourceDelegate(string url, out string contentType);
}
