using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Syncfusion.Data.Extensions;
using TwitterSQL.Utils;

namespace TwitterSQL.Models.Tables
{
    public class Users : ITable
    {
        public string TableName => "Users(Query: , Count: 20)";

        public IList<string> Columns => new List<string>
        {
            "User"
        };

        public IDictionary<string, string> Paramerters { get; set; }
        public string SelectPhrase { get; set; }
        public string WherePhrase { get; set; }
        public string GroupByPhrase { get; set; }
        public string HavingPhrase { get; set; }
        public string OrderByPhrase { get; set; }

        public Users()
        {
            Paramerters = new Dictionary<string, string>();
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
                Debug.WriteLine("NOT User");
                return (T)DynamicQueryable.Select(list.AsQueryable(), $"new({SelectPhrase})");
            }
            else
            {
                return (T)list;
            }
        }

        private async Task<IList<CoreTweet.User>> GetRawResult()
        {
            var query = Paramerters["Query"];
            var count = int.Parse(Paramerters["Count"]);
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
