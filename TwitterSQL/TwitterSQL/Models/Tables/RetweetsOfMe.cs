using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace TwitterSQL.Models.Tables
{
    public class RetweetsOfMe : ITable
    {
        public string TableName => "RetweetsOfMe(Count: 20)";
        public IList<string> Columns => new List<string>
        {
            "Tweet"
        };

        public IDictionary<string, string> Parameters { get; set; }
        public string SelectPhrase { get; set; }
        public string WherePhrase { get; set; }
        public string GroupByPhrase { get; set; }
        public string HavingPhrase { get; set; }
        public string OrderByPhrase { get; set; }

        public RetweetsOfMe()
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

            if (!string.IsNullOrEmpty(SelectPhrase) && !SelectPhrase.Equals("Tweet"))
            {
                return (T)list.AsQueryable().Select($"new({SelectPhrase})");
            }
            else
            {
                return (T)list;
            }
        }

        private async Task<IList<CoreTweet.Status>> GetRawResult()
        {
            var count = int.Parse(Parameters["Count"]);

            var tokens = await TokenGenerator.GenerateTokens();
            var result = await tokens.Statuses.RetweetsOfMeAsync(count: count);

            return result.ToList();
        }
    }
}
