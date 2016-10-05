using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using System.Diagnostics;
using System.Windows.Input;
using CoreTweet;
using Prism.Navigation;
using Reactive.Bindings;
using Syncfusion.Data.Extensions;
using Syncfusion.SfDataGrid.XForms;
using TwitterSQL.Models;
using TwitterSQL.Models.Tables;
using Xamarin.Forms;
using User = CoreTweet.User;
using TwitterSQL.Utils;

namespace TwitterSQL.ViewModels
{
    public class ResultPageViewModel : BindableBase, INavigationAware
    {
        private ITable _table;

        public ReactiveProperty<GridLength> ButtonWidthDataGrid { get; set; }
        public ReactiveProperty<GridLength> ButtonWidthTreeMap { get; set; }
        public ReactiveProperty<GridLength> ButtonWidthThree { get; set; }
        public ReactiveProperty<GridLength> ButtonWidthFour { get; set; }

        public ReactiveProperty<bool> IsVisibleTreeMap { get; set; }
        public ReactiveProperty<bool> IsVisibleDataGrid { get; set; }
        public ReactiveProperty<bool> IsBusy { get; set; }

        public ObservableCollection<dynamic> DataGridCollection { get; set; }
        public ObservableCollection<CustomTreeMapItem> TreeMapCollection { get; set; }

        public ICommand ShowDataGridCommand { get; }
        public ICommand ShowTreeMapCommand { get; }

        public ResultPageViewModel()
        {
            ButtonWidthDataGrid = new ReactiveProperty<GridLength>();
            ButtonWidthTreeMap = new ReactiveProperty<GridLength>();
            ButtonWidthThree = new ReactiveProperty<GridLength>();
            ButtonWidthFour = new ReactiveProperty<GridLength>();

            IsVisibleDataGrid = new ReactiveProperty<bool>();
            IsVisibleTreeMap = new ReactiveProperty<bool>();
            IsBusy = new ReactiveProperty<bool>();

            ButtonWidthDataGrid.Value = new GridLength(1, GridUnitType.Star);
            ButtonWidthTreeMap.Value = new GridLength(1, GridUnitType.Star);
            ButtonWidthThree.Value = new GridLength(1, GridUnitType.Star);
            ButtonWidthFour.Value = new GridLength(1, GridUnitType.Star);

            IsVisibleDataGrid.Value = false;
            IsVisibleTreeMap.Value = false;
            IsBusy.Value = true;

            ShowDataGridCommand = new Command(() =>
            {
                IsVisibleDataGrid.Value = true;
                IsVisibleTreeMap.Value = false;
            });

            ShowTreeMapCommand = new Command(() =>
            {
                IsVisibleDataGrid.Value = false;
                IsVisibleTreeMap.Value = true;
            });
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

            //var selectedColumns = _table.SelectPhrase.Replace(" ", "").Split(',');

            var list = await _table.GetResult<dynamic>();

            var collection = new ObservableCollection<dynamic>();
            foreach (var element in list)
            {
                Debug.WriteLine($"elemnt: {element}");
                collection.Add(element);
            }

            DataGridCollection = collection;
            IsVisibleDataGrid.Value = true;

            if (list.GetType() == typeof(List<CoreTweet.User>))
            {
                Debug.WriteLine($"User");

                var treeMapCollection = new ObservableCollection<CustomTreeMapItem>();
                int index = 0;

                foreach (User user in list)
                {
                    treeMapCollection.Add(new CustomTreeMapItem
                    {
                        WeightValue = (int) user.GetType().GetProperty(_table.OrderByPhrase.Split(' ')[0].Trim()).GetValue(user, null),
                        ImageSource = user.ProfileImageUrlHttps,
                        Text = user.Name
                    });

                    if(++index >= 10)
                        break;
                }

                TreeMapCollection = treeMapCollection;
            }
            else if (list.GetType() == typeof(List<CoreTweet.Status>))
            {
                Debug.WriteLine($"Status");

                var treeMapCollection = new ObservableCollection<CustomTreeMapItem>();
                int index = 0;

                foreach (Status status in list)
                {
                    treeMapCollection.Add(new CustomTreeMapItem
                    {
                        WeightValue = (int) status.GetType().GetProperty(_table.OrderByPhrase.Split(' ')[0].Trim()).GetValue(status, null),
                        ImageSource = status.User.ProfileImageUrlHttps,
                        Text = status.Text,
                    });

                    if(++index >= 10)
                        break;
                }

                TreeMapCollection = treeMapCollection;
            }

            IsBusy.Value = false;
        }
    }

    public class CustomTreeMapItem
    {
        public int WeightValue { get; set; }

        public ImageSource ImageSource { get; set; }

        public string Text { get; set; }
    }
}
