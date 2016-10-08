using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace TwitterSQL.Models.Tables
{
    public class Blocks : ITable
    {
        public string TableName => "Blocks";

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
            var tokens = await TokenGenerator.GenerateTokens();
            var result = await tokens.Blocks.ListAsync();

            return result.ToList();
        }
    }
}
