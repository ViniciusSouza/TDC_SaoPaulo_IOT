using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TDCSPMobileService.Models;
using System.Linq;
using TDCSPMobileService.DataObjects;
using TDCSPMobileService.Helper;
using System.Data.Entity;
using Microsoft.WindowsAzure.Mobile.Service.Notifications;
using System;


namespace TDCSPMobileService.ScheduledJobs
{
    // A simple scheduled job which can be invoked manually by submitting an HTTP
    // POST request to the path "/jobs/sample".

    public class SendPushMessageJob : ScheduledJob
    {
        private TDCSPMobileServiceContext context;
        public override  Task ExecuteAsync()
        {
            try
            {
                context = new TDCSPMobileServiceContext();
                var mensagem = string.Empty;

                var mensagens = context.PushMessages.Where(message => message.Enviado == false).ToList<PushMessage>();

                foreach (PushMessage pushMessage in mensagens)
                {
                    try
                    {
                        Toast toastMessage = new Toast();
                        toastMessage.Text1 = String.Format(pushMessage.Mensagem);

                        if (pushMessage.Microsoft)
                            PushHelper.SendMicrosoftPush(Services, toastMessage);

                        if (pushMessage.Android)
                            PushHelper.SendGooglePush(Services, pushMessage.Mensagem);
                    }
                    catch (System.Exception ex) { Services.Log.Info("Error Sending Push from SendPushMessageJob: " + ex.Message); }

                    Services.Log.Info(string.Format("SendPushMessageJob: Msg {0}  Microsoft{1}  Android{2}", pushMessage.Mensagem, pushMessage.Microsoft, pushMessage.Android));

                    pushMessage.Enviado = true;
                    context.Entry(pushMessage).State = EntityState.Modified;
                    context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                Services.Log.Info("Error Sending Push from SendPushMessageJob: " + ex.Message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}