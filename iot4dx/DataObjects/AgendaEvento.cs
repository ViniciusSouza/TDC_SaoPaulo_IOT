using iot4dx.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iot4dx.DataObjects
{
    [Table("AgendaEventoes", Schema = "iot_tdc_2014")]

    public class AgendaEvento : BaseMobile
    {
        public string Id { get; set; }

        [DisplayName("Palestrante(s)")]
        public String Palestrante { get; set; }

        [DisplayName("MiniBio Palestrante")]
        [DataType(DataType.MultilineText)]
        public String MiniBioPalestrante { get; set; }

        [DisplayName("Url Site Palestrante")]
        public String UrlSitePalestrante { get; set; }

        [DisplayName("Twitter Palestrante")]
        public String TwitterPalestrante { get; set; }

        [DisplayName("Url Foto Palestrante")]
        public String UrlFotoPalestrante { get; set; }

        [DisplayName("Nome Trilha TDC")]
        public String Trilha { get; set; }

        [DisplayName("Titulo Palestra")]
        public String Titulo { get; set; }

        [DisplayName("Descrição Palestra")]
        [DataType(DataType.MultilineText)]
        public String Descricao { get; set; }

        [DisplayName("Data Palestra")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} é um campo requirido no formato dd/MM/yyyy")]
        public DateTime Data { get; set; }

        [DisplayName("Hora Inicio")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} é um campo requirido no formato dd/MM/yyyy hh:mm")]
        public DateTime HoraInicio { get; set; }

        [DisplayName("Hora Término")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} é um campo requirido no formato dd/MM/yyyy hh:mm")]
        public DateTime HoraTermino { get; set; }

        public AgendaEvento()
        {

            Id = Guid.NewGuid().ToString();
            Data = DateHelper.Now();
            HoraInicio = DateHelper.Now();
            HoraTermino = DateHelper.Now();
            CreatedAt = DateHelper.Now();
            UpdatedAt = DateHelper.Now();
        }
    }
}