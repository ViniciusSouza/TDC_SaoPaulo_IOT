using Hammock.Authentication.OAuth;
using Hammock.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC2014WP.Twitter
{
    public class OAuthUtil
    {
        internal static OAuthWebQuery GetRequestTokenQuery()
        {
            var oauth = new OAuthWorkflow
            {
                ConsumerKey = TwitterSettings.consumerKey,
                ConsumerSecret = TwitterSettings.consumerKeySecret,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                RequestTokenUrl = TwitterSettings.RequestTokenUri,
                Version = TwitterSettings.oAuthVersion,
                CallbackUrl = TwitterSettings.CallbackUri
            };

            var info = oauth.BuildRequestTokenInfo(WebMethod.Get);
            var objOAuthWebQuery = new OAuthWebQuery(info, false);
            objOAuthWebQuery.HasElevatedPermissions = true;
            objOAuthWebQuery.SilverlightUserAgentHeader = "Hammock";
            return objOAuthWebQuery;
        }

        internal static OAuthWebQuery GetAccessTokenQuery(string requestToken, string RequestTokenSecret, string oAuthVerificationPin)
        {
            var oauth = new OAuthWorkflow
            {
                AccessTokenUrl = TwitterSettings.AccessTokenUri,
                ConsumerKey = TwitterSettings.consumerKey,
                ConsumerSecret = TwitterSettings.consumerKeySecret,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                Token = requestToken,
                Verifier = oAuthVerificationPin,
                Version = TwitterSettings.oAuthVersion
            };

            var info = oauth.BuildAccessTokenInfo(WebMethod.Post);
            var objOAuthWebQuery = new OAuthWebQuery(info, false);
            objOAuthWebQuery.HasElevatedPermissions = true;
            objOAuthWebQuery.SilverlightUserAgentHeader = "Hammock";
            return objOAuthWebQuery;
        }
    }
}
