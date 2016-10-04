using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Reactive.Bindings;
using TwitterSQL.Models;
using Xamarin.Forms;

namespace TwitterSQL.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public ReactiveProperty<string> SelectText { get; set; }
        public ReactiveProperty<string> FromText { get; set; }
        public ReactiveProperty<string> WhereText { get; set; }
        public ReactiveProperty<string> GroupByText { get; set; }
        public ReactiveProperty<string> HavingText { get; set; }
        public ReactiveProperty<string> OrderByText { get; set; }

        public QueryController QueryController { get; set; }

        public ICommand NavigateSearchCommand { get; }
        public ICommand FromTextChangedCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            QueryController = new QueryController();

            SelectText = new ReactiveProperty<string>();
            FromText = new ReactiveProperty<string>();
            WhereText = new ReactiveProperty<string>();
            GroupByText = new ReactiveProperty<string>();
            HavingText = new ReactiveProperty<string>();
            OrderByText = new ReactiveProperty<string>();

            NavigateSearchCommand = new DelegateCommand(() =>
            {
                Debug.WriteLine("Called: NavigateSearchCommand");

                var navigationParameters = new NavigationParameters
                {
                    ["SelectText"] = SelectText.Value,
                    ["FromText"] = FromText.Value,
                    ["WhereText"] = WhereText.Value,
                    ["GroupByText"] = GroupByText.Value,
                    ["HavingText"] = HavingText.Value,
                    ["OrderByText"] = OrderByText.Value
                };
                _navigationService.NavigateAsync("ResultPage", navigationParameters);
                //_navigationService.NavigateAsync("ApiTestPage");
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
