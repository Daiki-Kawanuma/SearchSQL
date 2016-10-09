using System;
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
    public class UserResultPageViewModel : BindableBase, INavigationAware
    {
        public event EventHandler<EventArgs> BindDataset;

        private ITable _table;

        public ReactiveProperty<GridLength> ButtonWidthDataGrid { get; set; }
        public ReactiveProperty<GridLength> ButtonWidthList { get; set; }
        public ReactiveProperty<GridLength> ButtonWidthTreeMap { get; set; }
        public ReactiveProperty<GridLength> ButtonWidthFour { get; set; }

        public ReactiveProperty<bool> IsVisibleDataGrid { get; set; }
        public ReactiveProperty<bool> IsVisibleList { get; set; }
        public ReactiveProperty<bool> IsVisibleTreeMap { get; set; }
        public ReactiveProperty<bool> IsBusy { get; set; }

        public ObservableCollection<dynamic> DataGridCollection { get; set; }
        public List<User> ListSource { get; set; }
        public List<CustomTreeMapItem> TreeMapList { get; set; }

        public ICommand ShowDataGridCommand { get; }
        public ICommand ShowListCommand { get; }
        public ICommand ShowTreeMapCommand { get; }

        public UserResultPageViewModel()
        {
            ButtonWidthDataGrid = new ReactiveProperty<GridLength>();
            ButtonWidthList = new ReactiveProperty<GridLength>();
            ButtonWidthTreeMap = new ReactiveProperty<GridLength>();
            ButtonWidthFour = new ReactiveProperty<GridLength>();

            IsVisibleDataGrid = new ReactiveProperty<bool>();
            IsVisibleList = new ReactiveProperty<bool>();
            IsVisibleTreeMap = new ReactiveProperty<bool>();
            IsBusy = new ReactiveProperty<bool>();

            ButtonWidthDataGrid.Value = new GridLength(0);
            ButtonWidthTreeMap.Value = new GridLength(0);
            ButtonWidthList.Value = new GridLength(0);
            ButtonWidthFour.Value = new GridLength(0);

            IsVisibleDataGrid.Value = false;
            IsVisibleList.Value = false;
            IsVisibleTreeMap.Value = false;
            IsBusy.Value = true;

            ShowDataGridCommand = new Command(() =>
            {
                IsVisibleDataGrid.Value = true;
                IsVisibleList.Value = false;
                IsVisibleTreeMap.Value = false;
            });

            ShowListCommand = new Command(() =>
            {
                IsVisibleDataGrid.Value = false;
                IsVisibleList.Value = true;
                IsVisibleTreeMap.Value = false;
            });

            ShowTreeMapCommand = new Command(() =>
            {
                IsVisibleDataGrid.Value = false;
                IsVisibleList.Value = false;
                IsVisibleTreeMap.Value = true;
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            _table = parameters["table"] as ITable;

            var list = await _table.GetResult<dynamic>();

            Debug.WriteLine("List.Count: " + list.Count);

            #region Set DataGrid Collection
            var collection = new ObservableCollection<dynamic>();
            foreach (var element in list)
            {
                collection.Add(element);
            }
            DataGridCollection = collection;

            ButtonWidthDataGrid.Value = new GridLength(1, GridUnitType.Star);
            #endregion

            if (list.GetType() == typeof(List<CoreTweet.User>))
            {
                #region Set ListView ItemSource
                ListSource = list;
                ButtonWidthList.Value = new GridLength(1, GridUnitType.Star);
                #endregion

                #region Set TreeMap Data source
                if (!string.IsNullOrEmpty(_table.OrderByPhrase))
                {

                    var treeMapList = new List<CustomTreeMapItem>();
                    int index = 0;

                    foreach (User user in list)
                    {
                        treeMapList.Add(new CustomTreeMapItem
                        {
                            WeightValue = (int)user.GetType().GetProperty(_table.OrderByPhrase.Split(' ')[0].Trim()).GetValue(user, null),
                            ImageSource = user.ProfileImageUrl,
                            Text = user.Name
                        });

                        if (++index >= 10)
                            break;
                    }

                    TreeMapList = treeMapList;
                    ButtonWidthTreeMap.Value = new GridLength(1, GridUnitType.Star);
                }
                #endregion
            }

            BindDataset?.Invoke(this, EventArgs.Empty);
        }

        public void OpenUserUrl(string url)
        {
            var uri = new Uri(url);
            Device.OpenUri(uri);
        }
    }

    public class CustomTreeMapItem
    {
        public int WeightValue { get; set; }

        public ImageSource ImageSource { get; set; }

        public string Text { get; set; }
    }
}
