using Prism.Unity;
using TwitterSQL.Views;
using Xamarin.Forms;

namespace TwitterSQL
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/SplashPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<SplashPage>();
            Container.RegisterTypeForNavigation<AuthConfigurationPage>();
            Container.RegisterTypeForNavigation<RootPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<HelpPage>();
            Container.RegisterTypeForNavigation<AboutPage>();
            Container.RegisterTypeForNavigation<ApiTestPage>();
            Container.RegisterTypeForNavigation<UserResultPage>();
            Container.RegisterTypeForNavigation<TweetResultPage>();
        }
    }
}