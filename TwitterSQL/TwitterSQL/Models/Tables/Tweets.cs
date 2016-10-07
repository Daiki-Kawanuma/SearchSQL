using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSQL.Models.Tables
{
    public class Tweets : ITable
    {
        public string TableName => "Tweets(Query: , Count: 20, ResultType: mixed, Lang: ja)";
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

        public Tweets()
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
                Debug.WriteLine("NOT Tweet");
                return (T)list.AsQueryable().Select($"new({SelectPhrase})");
            }
            else
            {
                return (T)list;
            }
        }

        private async Task<IList<CoreTweet.Status>> GetRawResult()
        {
            var query = Parameters["Query"];
            var count = int.Parse(Parameters["Count"]);
            var resultType = Parameters["ResultType"];
            var lang = Parameters["Lang"];

            var tokens = await TokenGenerator.GenerateTokens();

            var result = await tokens.Search.TweetsAsync(q: query, count: count, result_type: resultType, lang: lang);

            return result.ToList();
        }
    }
}
