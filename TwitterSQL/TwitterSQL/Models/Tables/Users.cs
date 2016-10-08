using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace TwitterSQL.Models.Tables
{
    public class Users : ITable
    {
        public string TableName => "Users(Query: , Count: 20)";

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

        public Users()
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
            var query = Parameters["Query"];
            var count = int.Parse(Parameters["Count"]);
            var page = (count / 20);

            var tokens = await TokenGenerator.GenerateTokens();

            var ret = new List<CoreTweet.User>();

            for (int i = 1; i <= page; i++)
            {
                var result = await tokens.Users.SearchAsync(q: query, count: count, page: i);
                ret.AddRange(result.ToList());
            }

            return ret;
        }
    }
}
