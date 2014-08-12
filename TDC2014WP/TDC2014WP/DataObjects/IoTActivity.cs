using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC2014WP.DataObjects
{
    public class IoTActivity
    {
        public String Id { get; set; }

        [JsonProperty(PropertyName = "color")]
        public MicrosoftColors Color { get; set; }

        [JsonProperty(PropertyName = "user")]
        public String User { get; set; }

        [JsonProperty(PropertyName = "urimage")]
        public String UriImage { get; set; }

        [JsonProperty(PropertyName = "complete")]
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
