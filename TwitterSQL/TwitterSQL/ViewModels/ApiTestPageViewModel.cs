using Prism.Mvvm;
using System.Diagnostics;
using System.Linq;
using CoreTweet;
using Reactive.Bindings;
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
            _tokens = await TokenGenerator.GenerateTokens();
            var results = await _tokens.Statuses.UserTimelineAsync(screen_name: "Santea3173", count: 40);
            Status s = results[0];

            /*var grouped = results.AsQueryable()
                .GroupBy("FavoriteCount", "it")
                .Select("new (it.Key as FavoriteCount, it.Count() as Number)")
                ;*/

            //var grouped = results.AsQueryable().GroupBy("FavoriteCount", "it");

            //Debug.WriteLine($"Avg: {results.AsQueryable().Aggregate("Average", "FavoriteCount")}");

            /*foreach (IGrouping<dynamic, dynamic> group in grouped)
            {
                //Debug.WriteLine($"{group.Key}, {group.Average(v => v.FavoriteCount)}");
                Debug.WriteLine($"{group.Key}, {group.Count()}");
            }*/

            var grouped = results.AsQueryable();

            var having = results.GroupBy(v => v.FavoriteCount).Where(a => a.Average(b => b.FavoriteCount) > 500);

            foreach (dynamic group in grouped)
            {
                // having
                Debug.WriteLine("Count: " + group.FavoriteCount + ", " + group.RetweetCount + ", " + group.Total);

                /*foreach (dynamic status in group.Statuses)
                {
                    Debug.WriteLine($"FavoriteCount: {group.FavoriteCount}, Text: {status.Text}");
                }*/
            }
        }
    }
}
