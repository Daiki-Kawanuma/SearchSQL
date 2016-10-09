using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace TwitterSQL.Models.Tables
{
    public class ListSubscribers : ITable
    {
        public string TableName => "ListSubscribers(Slug: , OwnerUserName: , Count: 20)";
        public IList<string> Columns => new List<string>
        {
            "User"
        };

        public IDictionary<string, string> Parameters { get; set; }
        public string SelectPhrase { get; set; }
        public string WherePhrase { get; set; }
        public string GroupByPhrase { get; set; }
        public string HavingPhrase { get; set; }
        public string OrderByPhrase { get; set; }

        public ListSubscribers()
        {
            Parameters = new Dictionary<string, string>();
        }

        public async Task<T> GetResult<T>()
        {
            var list = await GetRawResult();

            if (!string.IsNullOrEmpty(WherePhrase))
                list = list.AsQueryable().Where(WherePhrase).ToList();

            if (!string.IsNullOrEmpty(OrderByPhrase))
                list = list.AsQueryable().OrderBy(OrderByPhrase).ToList();

            if (!string.IsNullOrEmpty(SelectPhrase) && !SelectPhrase.Equals("User"))
            {
                return (T)list.AsQueryable().Select($"new({SelectPhrase})");
            }
            else
            {
                return (T)list;
            }
        }

        private async Task<IList<CoreTweet.User>> GetRawResult()
        {
            var slug = Parameters["Slug"];
            var ownerUserName = Parameters["OwnerUserName"];
            var count = int.Parse(Parameters["Count"]);

            var tokens = await TokenGenerator.GenerateAccessTokens();
            var result = await tokens.Lists.Subscribers.ListAsync(slug: slug, owner_screen_name: ownerUserName, count: count > 5000 ? 5000 : count);

            var returnList = new List<CoreTweet.User>();
            returnList.AddRange(result.ToList());

            while (returnList.Count < count && result.NextCursor != 0)
            {
                var requestCount = (count - returnList.Count) % 5001;
                result = await tokens.Lists.Subscribers.ListAsync(slug: slug, owner_screen_name: ownerUserName, count: requestCount, cursor: result.NextCursor);
                returnList.AddRange(result.ToList());
            }

            return returnList;
        }
    }
}
