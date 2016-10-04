using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label()
                    {
                        Text = "About"
                    }
                }
            };
        }
    }
}
