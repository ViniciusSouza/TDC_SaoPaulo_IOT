using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TDCSPMobileService.DataObjects;
using TDCSPMobileService.Helper;
using TDCSPMobileService.Models;

namespace TDCSPMobileService.ScheduledJobs
{
    // A simple scheduled job which can be invoked manually by submitting an HTTP
    // POST request to the path "/jobs/Push".

    public class PushJob : ScheduledJob
    {
        private TDCSPMobileServiceContext context;
        public override Task ExecuteAsync()
        {
            try
            {
                context = new TDCSPMobileServiceContext();
                var DataAtual = DateHelper.Now();
                var mensagem = string.Empty;
                var mensagem2 = string.Empty;


                foreach (AgendaEvento agenda in context.AgendaEventoes.ToList<AgendaEvento>())
                {
                    if (agenda.HoraInicio.Subtract(DataAtual).TotalMinutes > 0 && agenda.HoraInicio.Subtract(DataAtual).TotalMinutes <= 10)
                    {
                        mensagem = String.Format("Não Perca! Trilha: {0}; Palestrante {1}; Titulo {2} ", agenda.Trilha, agenda.Palestrante, agenda.Titulo);
                        mensagem2 = String.Format("Trilha: {0}; Palestrante {1}; Titulo {2} ", agenda.Trilha, agenda.Palestrante, agenda.Titulo);

                        Toast toastMessage = new Toast();
                        toastMessage.Text1 = String.Format("Não Perca! Trilha: {0}", agenda.Trilha);
                        toastMessage.Text2 = String.Format("Palestrante {0} - {1}", agenda.Palestrante, agenda.Titulo);

                        PushHelper.SendMicrosoftPush(Services, toastMessage);
                        PushHelper.SendGooglePush(Services, mensagem);

                        Services.Log.Info("Sending Push from JobScheduler");
                    }
                }
            }
            catch (Exception ex)
            {
                Services.Log.Info("Error Sending Push from JobScheduler: " + ex.Message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}