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
                    _table?.Parameters.Add(keyAndValue[0].Trim(), keyAndValue[1].Trim());
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
            rawText = rawText.Replace($"{Tweet.ObjectName}.{User.ObjectName}.", $"{Tweet.ObjectName}.{User.ObjectName}#");
            rawText = rawText.Replace($"{User.ObjectName}.{Tweet.ObjectName}.", $"{User.ObjectName}.{Tweet.ObjectName}#");

            rawText = rawText.Replace($"{Tweet.ObjectName}.", "");

            rawText = rawText.Replace($"{User.ObjectName}.", "");
            rawText = rawText.Replace(User.UserName, "ScreenName");
            rawText = rawText.Replace(User.FolloweeCount, "FriendsCount");
            rawText = rawText.Replace(User.LatestTweet, "Status");
            rawText = rawText.Replace(User.TweetsCount, "StatusesCount");

            rawText = rawText.Replace($"{List.ObjectName}.", "");

            rawText = rawText.Replace($"{Tweet.ObjectName}#", $"{Tweet.ObjectName}.");
            rawText = rawText.Replace($"{User.ObjectName}#", $"{User.ObjectName}.");

            return rawText;
        }
    }
}
