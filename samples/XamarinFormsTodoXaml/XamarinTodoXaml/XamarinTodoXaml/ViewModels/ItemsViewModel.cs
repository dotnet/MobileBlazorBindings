using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XamarinTodoXaml.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private string _newItemText;

        public ObservableCollection<TodoItem> Items { get; set; }
        public Command AddItemCommand { get; }

        public string NewItemText
        {
            get
            {
                return _newItemText;
            }
            set
            {
                if (_newItemText != value)
                {
                    _newItemText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<TodoItem>();
            AddItemCommand = new Command(() => ExecuteAddItemCommand());
        }

        public void LoadItems()
        {
            Items.Clear();
            var appState = DependencyService.Get<AppState>();
            if (appState.IsEmptyAppState())
            {
                appState.ResetAppState(createDefaultTodoItems: true);
            }
            var items = appState.Items;
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        private void ExecuteAddItemCommand()
        {
            var appState = DependencyService.Get<AppState>();

            var newItem = new TodoItem { Text = NewItemText };
            Items.Add(newItem);
            appState.Items.Add(newItem);

            NewItemText = null;
        }
    }
}
