using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using CoreTweet;
using TwitterSQL.Models.Tables;

namespace TwitterSQL.Models
{
    public class Tweet
    {
        public const string Contributors = "Contributors";
        public const string Coordinates = "Coordinates";
        public const string CreatedAt = "CreatedAt";
        public const string CurrentUserRetweet = "CurrentUserRetweet";
        public const string DisplayTextRange = "DisplayTextRange";
        public const string Entities = "Entities";
        public const string ExtendedEntities = "ExtendedEntities";
        public const string ExtendedTweet = "ExtendedTweet";
        public const string FavoriteCount = "FavoriteCount";
        public const string FilterLevel = "FilterLevel";
        public const string FullText = "FullText";
        public const string InReplyToScreenName = "InReplyToScreenName";
        public const string IsFavorited = "IsFavorited";
        public const string IsQuotedStatus = "IsQuotedStatus";
        public const string IsRetweeted = "IsRetweeted";
        public const string IsTruncated = "IsTruncated";
        public const string Language = "Language";
        public const string Place = "Place";
        public const string PossiblySensitive = "PossiblySensitive";
        public const string PossiblySensitiveAppealable = "PossiblySensitiveAppealable";
        public const string QuotedStatus = "QuotedStatus";
        public const string RetweetCount = "RetweetCount";
        public const string RetweetedStatus = "RetweetedStatus";
        public const string Scopes = "Scopes";
        public const string Source = "Source";
        public const string Text = "Text";
        public const string User = "User";
        public const string WithheldCopyright = "WithheldCopyright";
        public const string WithheldInCountries = "WithheldInCountries";
        public const string WithheldScope = "WithheldScope";

        public static string ObjectName = "Tweet";

        public static IList<string> Columns = new List<string>
        {
            $"{ObjectName}.{Contributors}",
            $"{ObjectName}.{Coordinates}",
            $"{ObjectName}.{CreatedAt}",
            $"{ObjectName}.{CurrentUserRetweet}",
            $"{ObjectName}.{DisplayTextRange}",
            $"{ObjectName}.{Entities}",
            $"{ObjectName}.{ExtendedEntities}",
            $"{ObjectName}.{ExtendedTweet}",
            $"{ObjectName}.{FavoriteCount}",
            $"{ObjectName}.{FilterLevel}",
            $"{ObjectName}.{FullText}",
            $"{ObjectName}.{InReplyToScreenName}",
            $"{ObjectName}.{IsFavorited}",
            $"{ObjectName}.{IsQuotedStatus}",
            $"{ObjectName}.{IsRetweeted}",
            $"{ObjectName}.{IsTruncated}",
            $"{ObjectName}.{Language}",
            $"{ObjectName}.{Place}",
            $"{ObjectName}.{PossiblySensitive}",
            $"{ObjectName}.{PossiblySensitiveAppealable}",
            $"{ObjectName}.{QuotedStatus}",
            $"{ObjectName}.{RetweetCount}",
            $"{ObjectName}.{RetweetedStatus}",
            $"{ObjectName}.{Scopes}",
            $"{ObjectName}.{Source}",
            $"{ObjectName}.{Text}",
            $"{ObjectName}.{User}",
            $"{ObjectName}.{WithheldCopyright}",
            $"{ObjectName}.{WithheldInCountries}",
            $"{ObjectName}.{WithheldScope}"
        };

        public static void GetDummyResult(ITable table)
        {
            var list = new List<Status>
            {
                new Status()
            };

            if (!string.IsNullOrEmpty(table.WherePhrase))
                list = list.AsQueryable().Where(table.WherePhrase).ToList();

            if (!string.IsNullOrEmpty(table.OrderByPhrase))
                list = list.AsQueryable().OrderBy(table.OrderByPhrase).ToList();

            if (!string.IsNullOrEmpty(table.SelectPhrase) && !table.SelectPhrase.Equals("Tweet"))
            {
                list.AsQueryable().Select($"new({table.SelectPhrase})");
            }
        }
    }
}
