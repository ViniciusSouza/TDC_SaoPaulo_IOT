using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iot4dx.Helper
{
    public static class DateHelper
    {

        public static DateTime Now()
        {
            DateTime timeUtc = DateTime.UtcNow;
            DateTime bztTime = timeUtc.AddHours(-3);

            return bztTime;
        }


        public static DateTime ToBrazil(DateTime date)
        {
            DateTime bztTime = date.AddHours(3);
            return bztTime;
        }
    }
}