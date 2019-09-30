using System.ComponentModel;

namespace XamarinTodoXaml
{
    public class TodoItem : INotifyPropertyChanged
    {
        private bool _isDone;
        private string _text;

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone != value)
                {
                    _isDone = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDone)));
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
