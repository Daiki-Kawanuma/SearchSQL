using Prism.Mvvm;
using System.Reactive.Linq;
using Akavache;
using CoreTweet;
using Prism.Navigation;
using Reactive.Bindings;
using TwitterSQL.Keys;
using Xamarin.Forms;

namespace TwitterSQL.ViewModels
{
    public class AuthConfigurationPageViewModel : BindableBase
    {
        public const string Callback = "http://127.0.0.1:64003/Account/ExternalLoginCallback";

        public OAuth.OAuthSession Session { get; set; }

        public ReactiveProperty<WebViewSource> Source { get; set; }

        public INavigationService NavigationService { get; set; }

        public AuthConfigurationPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            this.Source = new ReactiveProperty<WebViewSource>();
            ConfigureAuth();
        }

        private async void ConfigureAuth()
        {
            Session = await OAuth.AuthorizeAsync(consumerKey: TwitterApiKey.ConsumerKey, consumerSecret: TwitterApiKey.ConsumerSecret, oauthCallback: Callback);
            Source.Value = Session.AuthorizeUri;
        }
    }
}
