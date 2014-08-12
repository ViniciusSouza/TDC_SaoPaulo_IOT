using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TDCSPMobileService.DataObjects;
using TDCSPMobileService.Helper;
using TDCSPMobileService.Models;

namespace TDCSPMobileService.Controllers
{
    public class AgendaPushController : ApiController
    {
        public ApiServices Services { get; set; }

        private TDCSPMobileServiceContext context;

        // GET api/AgendaPush
        public string Get()
        {

            context = new TDCSPMobileServiceContext();
            var DataAtual = DateHelper.Now();
            var mensagem = string.Empty;

            foreach (AgendaEvento agenda in context.AgendaEventoes.ToList<AgendaEvento>())
            {
                if (DataAtual.Subtract(agenda.HoraInicio).TotalMinutes > 0 && DataAtual.Subtract(agenda.HoraInicio).TotalMinutes <= 10)
                {
                    mensagem = String.Format("Próxima palestra trilha: {0}; Palestrante {1}; Titulo {2} ", agenda.Trilha, agenda.Palestrante, agenda.Titulo);

                    //PushHelper.SendMicrosoftPush(Services, mensagem);
                    //PushHelper.SendGooglePush(Services, mensagem);

                    Services.Log.Info("Sending Push");
                }
            }

            

            
            return "Hello";
        }

    }
}
