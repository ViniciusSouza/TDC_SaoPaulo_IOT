using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitterizer;

namespace iot4dx.Twitter
{
    public static class TwitterHelper
    {
        public static void TwitteStatus(String message)
        {
            OAuthTokens tokens = Configuration.GetTokens();
            StatusUpdateOptions options = new StatusUpdateOptions();

            var response = TwitterAccount.VerifyCredentials(tokens, new VerifyCredentialsOptions
            {
                IncludeEntities = true,
                UseSSL = true
            });



            TwitterStatus newStatus = TwitterStatus.Update(tokens, message, options).ResponseObject;
        }

    }
}