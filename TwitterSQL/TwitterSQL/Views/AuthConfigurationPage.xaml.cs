using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Akavache;
using CoreTweet;
using TwitterSQL.ViewModels;
using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class AuthConfigurationPage : ContentPage
    {
        public AuthConfigurationPage()
        {
            InitializeComponent();

            this.WebView.PropertyChanged += PropertyChanged;
        }

        public new async void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Source")
            {
                var webView = sender as WebView;

                var source = webView?.Source as UrlWebViewSource;

                if (source != null && source.Url.Contains(AuthConfigurationPageViewModel.Callback))
                {
                    this.WebView.PropertyChanged -= PropertyChanged;

                    var verifier =
                        source.Url.Split('&').First(x => x.Contains("oauth_verifier")).Split('=')[1];

                    var context = this.BindingContext as AuthConfigurationPageViewModel;

                    if (context != null)
                    {
                        var token = await context.Session.GetTokensAsync(verifier);
                        await BlobCache.LocalMachine.InsertObject(PreserveAttribute.AccessToken, token.AccessToken);
                        await BlobCache.LocalMachine.InsertObject(PreserveAttribute.AccessTokenSecret, token.AccessTokenSecret);

                        await context.NavigationService.NavigateAsync("/RootPage/NavigationPage/MainPage");
                    }
                }
            }
        }
    }
}