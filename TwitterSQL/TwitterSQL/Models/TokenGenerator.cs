using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using CoreTweet;
using TwitterSQL.Keys;


namespace TwitterSQL.Models
{
    public class TokenGenerator
    {
        public static async Task<Tokens> GenerateTokens()
        {
            var accessToken = await BlobCache.LocalMachine.GetObject<string>(PreserveAttribute.AccessToken);
            var accessSecret = await BlobCache.LocalMachine.GetObject<string>(PreserveAttribute.AccessTokenSecret);

            return Tokens.Create(consumerKey: TwitterApiKey.ConsumerKey, consumerSecret: TwitterApiKey.ConsumerSecret, accessToken: accessToken, accessSecret: accessSecret);
        }
    }
}
