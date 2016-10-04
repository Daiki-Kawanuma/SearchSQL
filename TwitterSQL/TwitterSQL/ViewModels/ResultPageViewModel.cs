using System.Collections.Generic;
using Prism.Mvvm;
using System.Diagnostics;
using Prism.Navigation;
using Reactive.Bindings;
using TwitterSQL.Models;
using TwitterSQL.Models.Tables;
using User = CoreTweet.User;

namespace TwitterSQL.ViewModels
{
    public class ResultPageViewModel : BindableBase, INavigationAware
    {
        private ITable _table;

        public ReactiveProperty<string> Text { get; set; }

        public ResultPageViewModel()
        {
            Text = new ReactiveProperty<string>();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            _table = QueryParser.Parse((string)parameters["SelectText"],
                (string)parameters["FromText"],
                (string)parameters["WhereText"],
                (string)parameters["GroupByText"],
                (string)parameters["HavingText"],
                (string)parameters["OrderByText"]);

            var selectedColumns = _table.SelectPhrase.Replace(" ", "").Split(',');

            var list = await _table.GetResult<dynamic>();

            foreach (dynamic value in list)
            {
                string text = "";

                foreach (var column in selectedColumns)
                {
                    text += $"{value.GetType().GetProperty(column).GetValue(value, null)} ";
                }
                Text.Value += text + System.Environment.NewLine;
            }
        }
    }
}
