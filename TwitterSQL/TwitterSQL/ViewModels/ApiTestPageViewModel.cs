using Prism.Mvvm;
using System.Diagnostics;
using System.Linq;
using CoreTweet;
using Reactive.Bindings;
using Syncfusion.Data.Extensions;
using TwitterSQL.Models;

namespace TwitterSQL.ViewModels
{
    public class ApiTestPageViewModel : BindableBase
    {
        private Tokens _tokens;

        public ReactiveProperty<string> Text { get; set; }

        public ApiTestPageViewModel()
        {
            Text = new ReactiveProperty<string>();

            GetHomeTimeLine();
        }

        private async void GetHomeTimeLine()
        {
            var tokens = await TokenGenerator.GenerateAccessTokens();
            var result = await tokens.Lists.ShowAsync(slug: "xamarin", owner_screen_name: "amay077");

            foreach (var property in result.GetType().GetProperties())
            {
                Debug.WriteLine($"{property.Name}: {property.GetValue(result, null)}");
            }
            Debug.WriteLine("User: " + result.User.Name);
        }
    }
}
