using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC2014WP.Twitter
{
    public class TwitterSettings
    {
        // twitter
        public static string RequestTokenUri = "https://api.twitter.com/oauth/request_token";
        public static string AuthorizeUri = "https://api.twitter.com/oauth/authorize";
        public static string AccessTokenUri = "https://api.twitter.com/oauth/access_token";
        public static string CallbackUri = "http://viniciussouza.azurewebsites.net";   

        public static string consumerKey = "Your Consumer Key";
        public static string consumerKeySecret = "Your Consumer Secret";
        public static string oAuthVersion = "1.0a";
    }
}
