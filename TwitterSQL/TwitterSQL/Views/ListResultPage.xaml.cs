using CoreTweet;
using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class ListResultPage : ContentPage
    {
        private ListResultPageViewModel ViewModel => BindingContext as ListResultPageViewModel;

        public ListResultPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = BindingContext as ListResultPageViewModel;
            if (viewModel != null)
            {
                viewModel.BindDataset += (sender, args) =>
                {
                    DataGrid.ItemsSource = viewModel.DataGridCollection;
                    ListView.ItemsSource = viewModel.ListSource;
                    //TreeMap.DataSource = viewModel.TreeMapList;

                    viewModel.IsBusy.Value = false;
                    viewModel.IsVisibleDataGrid.Value = true;
                };
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = e.SelectedItem as List;
            if (list != null) ViewModel.OpenListUrl($"https://twitter.com{list.Uri}");
            ListView.SelectedItem = null;
        }
    }
}
