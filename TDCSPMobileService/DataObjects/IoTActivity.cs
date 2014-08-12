using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDCSPMobileService.DataObjects
{
    public class IoTActivity : EntityData
    {
        public MicrosoftColors Color { get; set; }

        public String User{ get; set; }

        public String UriImage { get; set; }

        public bool Complete { get; set; }


        public IoTActivity()
        {
            Color = MicrosoftColors.Red;
            User = string.Empty;
            UriImage = String.Empty;
            Complete = false;
        }

    }

    public enum MicrosoftColors
    {
        Red = 1,
        Green = 2,
        Blue = 3,
        Orange = 4
    }
}