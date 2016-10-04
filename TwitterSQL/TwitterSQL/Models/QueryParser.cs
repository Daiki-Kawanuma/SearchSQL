using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSQL.Models.Tables;

namespace TwitterSQL.Models
{
    public class QueryParser
    {
        private static ITable _table;

        public static ITable Parse(string selectText, string fromText, string whereText, string groupByText, string havingText, string orderByText)
        {
            ParseFromText(fromText);
            ParseSelectText(selectText);
            ParseWhereText(whereText);
            ParseOrderByText(orderByText);

            return _table;
        }

        private static void ParseSelectText(string selectText)
        {
            if (selectText == "Tweet" || selectText == "User")
            {
                _table.SelectPhrase = selectText;
                return;
            }

            var converted = ConvertText(selectText);

            _table.SelectPhrase = converted;
        }

        private static void ParseFromText(string fromText)
        {
            if (fromText.Contains("("))
            {
                var tableName = fromText.Substring(0, fromText.IndexOf("(", StringComparison.Ordinal));
                Debug.WriteLine($"TableName: {tableName}");

                var type = Type.GetType($"TwitterSQL.Models.Tables.{tableName}");
                _table = Activator.CreateInstance(type) as ITable;

                Debug.WriteLine(
                    $"Parameter: {fromText.Substring(fromText.IndexOf("(", StringComparison.Ordinal) + 1, fromText.IndexOf(")", StringComparison.Ordinal) - fromText.IndexOf("(", StringComparison.Ordinal) - 1)}");

                var parameters = fromText.Substring(fromText.IndexOf("(", StringComparison.Ordinal) + 1,
                    fromText.IndexOf(")", StringComparison.Ordinal) - fromText.IndexOf("(", StringComparison.Ordinal) - 1)
                    .Split(',');

                foreach (var parameter in parameters)
                {
                    var keyAndValue = parameter.Split(':');
                    Debug.WriteLine($"ParameterKey: {keyAndValue[0].Trim()}, ParameterValue: {keyAndValue[1].Trim()}");
                    _table?.Paramerters.Add(keyAndValue[0].Trim(), keyAndValue[1].Trim());
                }
            }
            else
            {
                var type = Type.GetType($"TwitterSQL.Models.Tables.{fromText}");

                if (type != null)
                    _table = Activator.CreateInstance(type) as ITable;
            }
        }

        private static void ParseWhereText(string whereText)
        {
            if (whereText == null)
                return;

            var converted = ConvertText(whereText);

            _table.WherePhrase = converted;
        }

        private static void ParseOrderByText(string orderByText)
        {
            if (orderByText == null)
                return;

            var converted = ConvertText(orderByText);

            _table.OrderByPhrase = converted;
        }

        private static string ConvertText(string rawText)
        {
            var converted = rawText.Replace($"{Tweet.ObjectName}.", "");

            converted = converted.Replace($"{User.ObjectName}.", "");
            converted = converted.Replace(User.UserName, "ScreenName");
            converted = converted.Replace(User.FolloweeCount, "FriendsCount");
            converted = converted.Replace(User.LatestTweet, "Status");
            converted = converted.Replace(User.TweetsCount, "StatusesCount");

            converted = converted.Replace($"{List.ObjectName}.", "");

            return converted;
        }
    }
}
