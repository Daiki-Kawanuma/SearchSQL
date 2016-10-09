using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using CoreTweet;
using Prism.Navigation;
using Prism.Services;
using Reactive.Bindings;
using Syncfusion.Data.Extensions;
using TwitterSQL.Models.Tables;
using Xamarin.Forms;

namespace TwitterSQL.ViewModels
{
    public class TweetResultPageViewModel : BindableBase, INavigationAware
    {
        public event EventHandler<EventArgs> BindDataset;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

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
        public List<Status> ListSource { get; set; }
        public List<CustomTreeMapItem> TreeMapList { get; set; }

        public ICommand ShowDataGridCommand { get; }
        public ICommand ShowListCommand { get; }
        public ICommand ShowTreeMapCommand { get; }

        public TweetResultPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

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

        public void OpenTweetUrl(string url)
        {
            var uri = new Uri(url);
            Device.OpenUri(uri);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            _table = parameters["table"] as ITable;

            try
            {
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

                if (list.GetType() == typeof(List<Status>))
                {
                    #region Set ListView ItemSource
                    ListSource = list;
                    ButtonWidthList.Value = new GridLength(1, GridUnitType.Star);
                    #endregion
                }

                BindDataset?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                await _pageDialogService.DisplayAlertAsync(e.GetType().Name, e.Message, "OK");
                await _navigationService.GoBackAsync();
            }
        }
    }
}
