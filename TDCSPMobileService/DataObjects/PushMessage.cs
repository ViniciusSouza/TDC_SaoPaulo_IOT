using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TDCSPMobileService.DataObjects
{
    [Table("PushMessage", Schema = "iot_tdc_2014")]
    public class PushMessage
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage="O campo mensagem é obrigatório!")]
        public String Mensagem { get; set; }

        public String Agent { get; set; }

        public bool Microsoft { get; set; }

        public bool Android { get; set; }

        public DateTime Data { get; set; }

        public bool Enviado { get; set; }

        public PushMessage()
        {
            Id = Guid.NewGuid().ToString();
            Microsoft = true;
            Android = true;
            Agent = "WebSite";
            Data = DateTime.Now;
            Enviado = false;
        }

    }
}