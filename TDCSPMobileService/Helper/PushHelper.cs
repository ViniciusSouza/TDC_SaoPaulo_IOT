using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDCSPMobileService.Helper
{
    public static class PushHelper
    {

        private static async void SendPush(ApiServices service, IPushMessage message)
        {

            try
            {
                PushClient cliente = new PushClient(service);
                await cliente.SendAsync(message);
            }
            catch (System.Exception ex)
            {
            }
        }

        public static async void SendMicrosoftPush(ApiServices service, string message, string message2)
        {

            Toast toastMessage = new Toast();
            toastMessage.Text1 = message;
            toastMessage.Text2 = message2;

            MpnsPushMessage microsoftPush = new MpnsPushMessage(toastMessage);

            //string windowsToastMsg = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><wp:Notification xmlns:wp=\"WPNotification\"><wp:Toast><wp:Text1>{0}</wp:Text1></wp:Toast></wp:Notification>", message);
            //microsoftPush.XmlPayload = windowsToastMsg;

            //PushClient  teste = new PushClient()

            SendPush(service, microsoftPush);

        }

        public static async void SendMicrosoftPush(ApiServices service, Toast toastMessage)
        {

            MpnsPushMessage microsoftPush = new MpnsPushMessage(toastMessage);

            //string windowsToastMsg = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><wp:Notification xmlns:wp=\"WPNotification\"><wp:Toast><wp:Text1>{0}</wp:Text1></wp:Toast></wp:Notification>", message);
            //microsoftPush.XmlPayload = windowsToastMsg;

            //PushClient  teste = new PushClient()

            SendPush(service, microsoftPush);

        }


        public static async void SendGooglePush(ApiServices service, string message)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("message", message);

            GooglePushMessage googlePushMessage = new GooglePushMessage(data, TimeSpan.FromHours(1));


            SendPush(service, googlePushMessage);
        }
    }
}