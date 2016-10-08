using CoreTweet;
using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class UserResultPage : ContentPage
    {
        private UserResultPageViewModel ViewModel => this.BindingContext as UserResultPageViewModel;

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
                    ListView.ItemsSource = viewModel.ListSource;
                    TreeMap.DataSource = viewModel.TreeMapList;

                    viewModel.IsBusy.Value = false;
                    viewModel.IsVisibleDataGrid.Value = true;
                    viewModel.IsVisibleTreeMap.Value = false;
                };
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var user = e.SelectedItem as User;
            if (user != null) ViewModel.OpenUserUrl("https://twitter.com/" + user.ScreenName);
            ListView.SelectedItem = null;
        }
    }
}
