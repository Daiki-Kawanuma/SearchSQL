using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using CoreTweet;
using TwitterSQL.Keys;


namespace TwitterSQL.Models
{
    public class TokenGenerator
    {
        public static async Task<Tokens> GenerateAccessTokens()
        {
            var accessToken = await BlobCache.LocalMachine.GetObject<string>(PreserveAttribute.AccessToken);
            var accessSecret = await BlobCache.LocalMachine.GetObject<string>(PreserveAttribute.AccessTokenSecret);

            return Tokens.Create(consumerKey: TwitterApiKey.ConsumerKey, consumerSecret: TwitterApiKey.ConsumerSecret, accessToken: accessToken, accessSecret: accessSecret);
        }

        public static async Task<OAuth2Token> GenerateBearerTokens()
        {
            var apponly = await OAuth2.GetTokenAsync(consumerKey: TwitterApiKey.ConsumerKey, consumerSecret: TwitterApiKey.ConsumerSecret);
            return OAuth2Token.Create(consumerKey: TwitterApiKey.ConsumerKey, consumerSecret: TwitterApiKey.ConsumerSecret, bearer: apponly.BearerToken);
        }
    }
}
