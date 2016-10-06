using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = BindingContext as ResultPageViewModel;
            if (viewModel != null)
            {
                viewModel.BindDataset += (sender, args) =>
                {
                    DataGrid.ItemsSource = viewModel.DataGridCollection;
                    TreeMap.DataSource = viewModel.TreeMapList;

                    viewModel.IsBusy.Value = false;
                    viewModel.IsVisibleDataGrid.Value = true;
                    viewModel.IsVisibleTreeMap.Value = false;
                };
            }
        }
    }
}
