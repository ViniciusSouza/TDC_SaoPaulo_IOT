using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDCSPMobileService.Helper
{
    public static class DateHelper
    {

        public static DateTime Now()
        {
            DateTime timeUtc = DateTime.UtcNow;
            DateTime bztTime = timeUtc.AddHours(-3);

            return bztTime;
        }

    }
}