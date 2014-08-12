using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot4dx.DataObjects
{
    [Table("IoTActivities", Schema = "iot_tdc_2014")]
    public class IoTActivity : BaseMobile
    {

        public String Id { get; set; }

        [DisplayName("Cor")]
        [JsonProperty(PropertyName = "color")]
        public MicrosoftColors Color { get; set; }

        [DisplayName("Usuário")]
        [JsonProperty(PropertyName = "user")]
        public String User { get; set; }

        [DisplayName("URI Imagem")]
        [JsonProperty(PropertyName = "urimage")]
        public String UriImage { get; set; }

        [DisplayName("Atividade Completa")]
        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }


        public IoTActivity()
        {
            Color = MicrosoftColors.Red;
            User = string.Empty;
            UriImage = String.Empty;
            Complete = false;
            Id = Guid.NewGuid().ToString();
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
