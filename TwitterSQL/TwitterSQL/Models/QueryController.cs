using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Syncfusion.SfTreeMap.XForms;
using TwitterSQL.Models.Tables;
using TwitterSQL.Utils;

namespace TwitterSQL.Models
{
    public class QueryController
    {
        public ObservableCollection<string> SelectSuggestions { get; set; }
        public ObservableCollection<string> FromSuggestions { get; set; }
        public ObservableCollection<string> WhereSuggestions { get; set; }
        public ObservableCollection<string> GroupBySuggestions { get; set; }
        public ObservableCollection<string> HavingSuggestions { get; set; }
        public ObservableCollection<string> OrderBySuggestions { get; set; }

        public QueryController()
        {
            SelectSuggestions = new ObservableCollection<string>();

            FromSuggestions = new ObservableCollection<string>()
            {
                new Blocks().TableName,
                new Favorites().TableName,
                new Followee().TableName,
                new Followers().TableName,
                new HomeTimeLine().TableName,
                new Tables.List().TableName,
                new ListMembers().TableName,
                new Lists().TableName,
                new ListSubscribers().TableName,
                new MembershipLists().TableName,
                new MentionsTimeLine().TableName,
                new Mutes().TableName,
                new RetweetsOfMe().TableName,
                new SubscriptionLists().TableName,
                new Tweets().TableName,
                new Users().TableName,
                new UserTimeLine().TableName
            };

            WhereSuggestions = new ObservableCollection<string>();
            //GroupBySuggestions = new ObservableCollection<string>();
            //HavingSuggestions = new ObservableCollection<string>();
            OrderBySuggestions = new ObservableCollection<string>();
        }

        public void OnFromTextChanged(string fromText)
        {
            SelectSuggestions.Clear();
            WhereSuggestions.Clear();
            //GroupBySuggestions.Clear();
            //HavingSuggestions.Clear();
            OrderBySuggestions.Clear();

            string tableName;

            if (fromText.Contains("("))
            {
                tableName = fromText.Substring(0, fromText.IndexOf("(", StringComparison.Ordinal));
            }
            else
            {
                tableName = fromText;
            }

            Debug.WriteLine($"TableName: {tableName}");

            var type = Type.GetType($"TwitterSQL.Models.Tables.{tableName}");

            if (type != null)
            {
                var table = Activator.CreateInstance(type) as ITable;

                if (table == null)
                    return;

                SelectSuggestions.AddRange(table.Columns);
                WhereSuggestions.AddRange(table.Columns);
                //GroupBySuggestions.AddRange(table.Columns);
                //HavingSuggestions.AddRange(table.Columns);
                OrderBySuggestions.AddRange(table.Columns);
            }

            if (SelectSuggestions.Count(x => x.Equals(User.ObjectName)) > 0)
            {
                SelectSuggestions.AddRange(User.Columns);
                WhereSuggestions.AddRange(User.Columns);
                //GroupBySuggestions.AddRange(User.Columns);
                //HavingSuggestions.AddRange(User.Columns);
                OrderBySuggestions.AddRange(User.Columns);
            }

            if (SelectSuggestions.Count(x => x.Equals(Tweet.ObjectName)) > 0)
            {
                SelectSuggestions.AddRange(Tweet.Columns);
                WhereSuggestions.AddRange(Tweet.Columns);
                //GroupBySuggestions.AddRange(Tweet.Columns);
                //HavingSuggestions.AddRange(Tweet.Columns);
                OrderBySuggestions.AddRange(Tweet.Columns);
            }

            if (SelectSuggestions.Count(x => x.Equals(List.ObjectName)) > 0)
            {
                SelectSuggestions.AddRange(List.Columns);
                WhereSuggestions.AddRange(List.Columns);
                //GroupBySuggestions.AddRange(List.Columns);
                //HavingSuggestions.AddRange(List.Columns);
                OrderBySuggestions.AddRange(List.Columns);
            }
        }
    }
}
