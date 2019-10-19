using SQLite;

namespace BlazorNativeTodo
{
    public class TodoItem
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        public bool IsDone { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }
    }
}
