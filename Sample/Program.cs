using System;

namespace Sample
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            BlinForms.Framework.BlinForms.Run<WinCounter>();
        }
    }
}
