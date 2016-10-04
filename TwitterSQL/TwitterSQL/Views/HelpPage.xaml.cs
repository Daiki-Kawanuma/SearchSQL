using Xamarin.Forms;

namespace TwitterSQL.Views
{
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
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
                        Text = "Help"
                    }
                }
            };
        }
    }
}
