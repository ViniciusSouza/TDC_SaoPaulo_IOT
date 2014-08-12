using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC2014WP.DataObjects
{
    public class AgendaEvento
    {
        public String Id { get; set; }


         [JsonProperty(PropertyName = "palestrante")]
        public String Palestrante { get; set; }

         [JsonProperty(PropertyName = "minibio")]
         public String MiniBio { get; set; }

         [JsonProperty(PropertyName = "urlsitepalestrante")]
        public String UrlSitePalestrante { get; set; }

         [JsonProperty(PropertyName = "twitterpalestrante")]
        public String  TwitterPalestrante { get; set; }

        [JsonProperty(PropertyName = "urlfotopalestrante")]
        public String UrlFotoPalestrante { get; set;}

        [JsonProperty(PropertyName = "trilha")]
        public String Trilha { get; set; }

        [JsonProperty(PropertyName = "titulo")]
        public String Titulo { get; set; }

        [JsonProperty(PropertyName = "descricao")]
        public String Descricao { get; set; }

        [JsonProperty(PropertyName = "data")]
        public DateTime Data{ get; set; }

        [JsonProperty(PropertyName = "horainicio")]
        public DateTime HoraInicio { get; set; }

        [JsonProperty(PropertyName = "horatermino")]
        public DateTime HoraTermino { get; set; }
    }
}