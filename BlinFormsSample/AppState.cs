using System.Collections.Generic;
using System.Linq;

namespace BlinFormsSample
{
    public class AppState
    {
        public List<TodoItem> items = new List<TodoItem>();

        public int Counter { get; set; }

        public void ResetAppState()
        {
            items.AddRange(
                new[]
                {
                    new TodoItem { Text = "sell dog", IsDone = true },
                    new TodoItem { Text = "buy cat" },
                    new TodoItem { Text = "buy cat food" },
                });

            Counter = 0;
        }

        public bool IsEmptyAppState()
        {
            return
                !items.Any() &&
                Counter == 0;
        }
    }
}
