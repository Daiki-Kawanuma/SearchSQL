using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterSQL.Models
{
    public class User
    {
        public const string CreatedAt = "CreatedAt";
        public const string Description = "Description";
        public const string Email = "Email";
        public const string FavoriteCount = "FavoritesCount";
        public const string FollowersCount = "FollowersCount";
        public const string FolloweeCount = "FolloweeCount";
        public const string IsContributorsEnabled = "IsContributorsEnabled";
        public const string IsDefaultProfile = "IsDefaultProfile";
        public const string IsDefaultProfileImage = "IsDefaultProfileImage";
        public const string IsFollowRequestSent = "IsFollowRequestSent";
        public const string IsGeoEnabled = "IsGeoEnabled";
        public const string IsMuting = "IsMuting";
        public const string IsProfileBackgroundTile = "IsProfileBackgroundTile";
        public const string IsProfileUseBackgroundImage = "IsProfileUseBackgroundImage";
        public const string IsProtected = "IsProtected";
        public const string IsShowAllInlineMedia = "IsShowAllInlineMedia";
        public const string IsSuspended = "IsSuspended";
        public const string IsTranslationEnabled = "IsTranslationEnabled";
        public const string IsTranslator = "IsTranslator";
        public const string IsVerified = "IsVerified";
        public const string Language = "Language";
        public const string ListedCount = "ListedCount";
        public const string Location = "Location";
        public const string Name = "Name";
        public const string NeedsPhoneVerification = "NeedsPhoneVerification";
        public const string ProfileBackgroundColor = "ProfileBackgroundColor";
        public const string ProfileBackgroundImageUrl = "ProfileBackgroundImageUrl";
        public const string ProfileBackgroundImageUrlHttps = "ProfileBackgroundImageUrlHttps";
        public const string ProfileBannerUrl = "ProfileBannerUrl";
        public const string ProfileImageUrl = "ProfileImageUrl";
        public const string ProfileImageUrlHttps = "ProfileImageUrlHttps";
        public const string ProfileLinkColor = "ProfileLinkColor";
        public const string ProfileLocation = "ProfileLocation";
        public const string ProfileSidebarBorderColor = "ProfileSidebarBorderColor";
        public const string ProfileSidebarFillColor = "ProfileSidebarFillColor";
        public const string ProfileTextColor = "ProfileTextColor";
        public const string UserName = "UserName";
        public const string LatestTweet = "LatestTweet";
        public const string TweetsCount = "TweetsCount";
        public const string TimeZone = "TimeZone";
        public const string Url = "Url";
        public const string UtcOffset = "UtcOffset";
        public const string WithheldInCountries = "WithheldInCountries";
        public const string WithheldScope = "WithheldScope";

        public static string ObjectName = "User";

        public static IList<string> Columns = new List<string>
        {
            $"{ObjectName}.{CreatedAt}",
            $"{ObjectName}.{Description}",
            $"{ObjectName}.{Email}",
            $"{ObjectName}.{FavoriteCount}",
            $"{ObjectName}.{FollowersCount}",
            $"{ObjectName}.{FolloweeCount}",
            $"{ObjectName}.{IsContributorsEnabled}",
            $"{ObjectName}.{IsDefaultProfile}",
            $"{ObjectName}.{IsDefaultProfileImage}",
            $"{ObjectName}.{IsGeoEnabled}",
            $"{ObjectName}.{IsMuting}",
            $"{ObjectName}.{IsProfileBackgroundTile}",
            $"{ObjectName}.{IsProfileUseBackgroundImage}",
            $"{ObjectName}.{IsProtected}",
            $"{ObjectName}.{IsShowAllInlineMedia}",
            $"{ObjectName}.{IsSuspended}",
            $"{ObjectName}.{IsTranslationEnabled}",
            $"{ObjectName}.{IsTranslator}",
            $"{ObjectName}.{IsVerified}",
            $"{ObjectName}.{Language}",
            $"{ObjectName}.{ListedCount}",
            $"{ObjectName}.{Location}",
            $"{ObjectName}.{Name}",
            $"{ObjectName}.{NeedsPhoneVerification}",
            $"{ObjectName}.{ProfileBackgroundColor}",
            $"{ObjectName}.{ProfileBackgroundImageUrl}",
            $"{ObjectName}.{ProfileBackgroundImageUrlHttps}",
            $"{ObjectName}.{ProfileBannerUrl}",
            $"{ObjectName}.{ProfileImageUrl}",
            $"{ObjectName}.{ProfileImageUrlHttps}",
            $"{ObjectName}.{ProfileLinkColor}",
            $"{ObjectName}.{ProfileLocation}",
            $"{ObjectName}.{ProfileSidebarBorderColor}",
            $"{ObjectName}.{ProfileSidebarFillColor}",
            $"{ObjectName}.{ProfileTextColor}",
            $"{ObjectName}.{UserName}",
            $"{ObjectName}.{LatestTweet}",
            $"{ObjectName}.{TweetsCount}",
            $"{ObjectName}.{TimeZone}",
            $"{ObjectName}.{Url}",
            $"{ObjectName}.{UtcOffset}",
            $"{ObjectName}.{WithheldInCountries}",
            $"{ObjectName}.{WithheldScope}"
        };
    }
}
