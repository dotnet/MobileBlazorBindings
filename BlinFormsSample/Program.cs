using System;

namespace BlinFormsSample
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            BlinForms.Framework.BlinForms.Run<TodoApp>();
        }
    }
}
