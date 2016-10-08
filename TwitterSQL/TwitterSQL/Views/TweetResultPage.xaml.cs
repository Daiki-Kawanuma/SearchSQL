using CoreTweet;
using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class TweetResultPage : ContentPage
    {
        private TweetResultPageViewModel ViewModel => this.BindingContext as TweetResultPageViewModel;

        public TweetResultPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = BindingContext as TweetResultPageViewModel;
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
            var status = e.SelectedItem as Status;
            if (status != null) ViewModel.OpenTweetUrl($"https://twitter.com/{status.User.ScreenName}/status/{status.Id}");
            ListView.SelectedItem = null;
        }
    }
}
