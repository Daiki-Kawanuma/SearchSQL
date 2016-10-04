using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;

namespace TwitterSQL.ViewModels
{
    public class RootPageViewModel : BindableBase
    {
        public ObservableCollection<MenuItem> Menus { get; } = new ObservableCollection<MenuItem>
        {
            new MenuItem
            {
                ImageSource = ImageSource.FromResource("TwitterSQL.Resources.Images.ic_favorite_border_black_24dp.png"),
                Title = "Main",
                PageName = "MainPage"
            },
            new MenuItem
            {
                ImageSource = ImageSource.FromResource("TwitterSQL.Resources.Images.ic_favorite_border_black_24dp.png"),
                Title = "Help",
                PageName = "HelpPage"
            },
            new MenuItem
            {
                ImageSource = ImageSource.FromResource("TwitterSQL.Resources.Images.ic_favorite_border_black_24dp.png"),
                Title = "About",
                PageName = "AboutPage"
            }
        };

        private readonly INavigationService _navigationService;

        private bool _isPresented;

        public bool IsPresented
        {
            get { return this._isPresented; }
            set { this.SetProperty(ref this._isPresented, value); }
        }

        public RootPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task PageChangeAsync(MenuItem menuItem)
        {
            await this._navigationService.NavigateAsync($"NavigationPage/{menuItem.PageName}");
            IsPresented = false;
        }
    }
}
