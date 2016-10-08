using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class UserResultPage : ContentPage
    {
        public UserResultPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = BindingContext as UserResultPageViewModel;
            if (viewModel != null)
            {
                viewModel.BindDataset += (sender, args) =>
                {
                    DataGrid.ItemsSource = viewModel.DataGridCollection;
                    this.ListView.ItemsSource = viewModel.ListSource;
                    TreeMap.DataSource = viewModel.TreeMapList;

                    viewModel.IsBusy.Value = false;
                    viewModel.IsVisibleDataGrid.Value = true;
                    viewModel.IsVisibleTreeMap.Value = false;
                };
            }
        }
    }
}
