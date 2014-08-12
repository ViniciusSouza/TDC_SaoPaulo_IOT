using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDCSPMobileService.DataObjects
{
    public class AgendaEvento : EntityData
    {

        public String Palestrante { get; set; }

        public String MiniBioPalestrante { get; set; }

        public String UrlSitePalestrante { get; set; }

        public String  TwitterPalestrante { get; set; }

        public String UrlFotoPalestrante { get; set;}

        public String Trilha { get; set; }

        public String Titulo { get; set; }

        public String Descricao { get; set; }

        public DateTime Data{ get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraTermino { get; set; }
    }
}