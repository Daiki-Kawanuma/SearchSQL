using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Services;
using Reactive.Bindings;
using TwitterSQL.Models;
using TwitterSQL.Models.Tables;
using Xamarin.Forms;
using List = TwitterSQL.Models.List;

namespace TwitterSQL.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        public ReactiveProperty<string> SelectText { get; set; }
        public ReactiveProperty<string> FromText { get; set; }
        public ReactiveProperty<string> WhereText { get; set; }
        public ReactiveProperty<string> GroupByText { get; set; }
        public ReactiveProperty<string> HavingText { get; set; }
        public ReactiveProperty<string> OrderByText { get; set; }

        public QueryController QueryController { get; set; }

        public ICommand NavigateSearchCommand { get; }
        public ICommand FromTextChangedCommand { get; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            QueryController = new QueryController();

            SelectText = new ReactiveProperty<string>();
            FromText = new ReactiveProperty<string>();
            WhereText = new ReactiveProperty<string>();
            GroupByText = new ReactiveProperty<string>();
            HavingText = new ReactiveProperty<string>();
            OrderByText = new ReactiveProperty<string>();

            NavigateSearchCommand = new DelegateCommand(() =>
            {
                try
                {
                    var table = QueryParser.Parse(SelectText.Value,
                        FromText.Value,
                        WhereText.Value,
                        GroupByText.Value,
                        HavingText.Value,
                        OrderByText.Value);

                    var navigationParameters = new NavigationParameters {{"table", table}};

                    if (table.Columns[0] == "User")
                    {
                        User.GetDummyResult(table);
                        navigationService.NavigateAsync("UserResultPage", navigationParameters);
                    }
                    else if (table.Columns[0] == "Tweet")
                    {
                        Tweet.GetDummyResult(table);
                        navigationService.NavigateAsync("TweetResultPage", navigationParameters);
                    }
                    else if (table.Columns[0] == "List")
                    {
                        List.GetDummyResult(table);
                        navigationService.NavigateAsync("ListResultPage", navigationParameters);
                    }
                }
                catch (Exception e)
                {
                    pageDialogService.DisplayAlertAsync(e.GetType().Name, e.Message, "OK");
                }
            });

            FromTextChangedCommand = new Command(() =>
            {
                QueryController.OnFromTextChanged(FromText.Value);
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
