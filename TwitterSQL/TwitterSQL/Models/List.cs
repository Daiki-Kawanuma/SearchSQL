using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;

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
    }
}
