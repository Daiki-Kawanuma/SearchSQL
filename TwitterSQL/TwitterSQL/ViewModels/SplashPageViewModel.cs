using Prism.Mvvm;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;
using Prism.Navigation;

namespace TwitterSQL.ViewModels
{
    public class SplashPageViewModel : BindableBase
    {
        public SplashPageViewModel(INavigationService navigationService)
        {
            Navigate(navigationService);
        }

        public async void Navigate(INavigationService navigationService)
        {
            try
            {
                var token = await BlobCache.LocalMachine.GetObject<string>(PreserveAttribute.AccessToken);
                await navigationService.NavigateAsync("/RootPage/NavigationPage/MainPage");
            }
            catch (KeyNotFoundException)
            {
                await navigationService.NavigateAsync("/AuthConfigurationPage");
            }
        }
    }
}