using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using TwitterSQL.Models.Tables;

namespace TwitterSQL.Models
{
    public class List
    {
        public const string CreatedAt = "CreatedAt";
        public const string Description = "Description";
        public const string FullName = "FullName";
        public const string IsFollowing = "IsFollowing";
        public const string MemberCount = "MemberCount";
        public const string Mode = "Mode";
        public const string Name = "Name";
        public const string Slug = "Slug";
        public const string SubscriberCount = "SubscriberCount";
        public const string Uri = "Uri";
        public const string User = "User";

        public const string ObjectName = "List";

        public static IList<string> Columns = new List<string>
        {
            $"{ObjectName}.{CreatedAt}",
            $"{ObjectName}.{Description}",
            $"{ObjectName}.{FullName}",
            $"{ObjectName}.{IsFollowing}",
            $"{ObjectName}.{MemberCount}",
            $"{ObjectName}.{Mode}",
            $"{ObjectName}.{Name}",
            $"{ObjectName}.{Slug}",
            $"{ObjectName}.{SubscriberCount}",
            $"{ObjectName}.{Uri}",
            $"{ObjectName}.{User}"
        };

        public static void GetDummyResult(ITable table)
        {
            var list = new List<CoreTweet.List>
            {
                new CoreTweet.List()
            };

            if (!string.IsNullOrEmpty(table.WherePhrase))
                list = list.AsQueryable().Where(table.WherePhrase).ToList();

            if (!string.IsNullOrEmpty(table.OrderByPhrase))
                list = list.AsQueryable().OrderBy(table.OrderByPhrase).ToList();

            if (!string.IsNullOrEmpty(table.SelectPhrase) && !table.SelectPhrase.Equals("List"))
            {
                list.AsQueryable().Select($"new({table.SelectPhrase})");
            }
        }
    }
}
