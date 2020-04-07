using System;
using System.Collections.Generic;
using System.IO;
using WebWindows.Blazor.XamarinForms;

namespace WebWindows.Blazor
{
    public delegate Stream ResolveWebResourceDelegate(string url, out string contentType);
}
