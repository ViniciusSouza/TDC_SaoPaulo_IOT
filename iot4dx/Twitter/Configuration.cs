using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Twitterizer;

namespace iot4dx.Twitter
{
    public static class Configuration
    {

        public static OAuthTokens GetTokens()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = HttpContext.Current.Server.MapPath("~/Twitter.config")
            };

            

            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);


            OAuthTokens tokens = new OAuthTokens();
            tokens.AccessToken = config.AppSettings.Settings["AccessToken"].Value;
            tokens.AccessTokenSecret = config.AppSettings.Settings["AccessTokenSecret"].Value;
            tokens.ConsumerKey = config.AppSettings.Settings["ConsumerKey"].Value;
            tokens.ConsumerSecret = config.AppSettings.Settings["ConsumerSecret"].Value;


            return tokens;
        }

    }
}