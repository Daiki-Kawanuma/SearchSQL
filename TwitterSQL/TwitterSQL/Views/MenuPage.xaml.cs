using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class MenuPage : ContentPage
    {
        private RootPageViewModel ViewModel => this.BindingContext as RootPageViewModel;

        public MenuPage()
        {
            InitializeComponent();
        }

        private async void ListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await ViewModel.PageChangeAsync(e.SelectedItem as ViewModels.MenuItem);
        }
    }
}
