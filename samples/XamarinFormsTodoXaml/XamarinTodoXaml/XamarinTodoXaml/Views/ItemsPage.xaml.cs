using System.ComponentModel;
using Xamarin.Forms;
using XamarinTodoXaml.ViewModels;

namespace XamarinTodoXaml.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
            {
                _viewModel.LoadItems();
            }
        }
    }
}
