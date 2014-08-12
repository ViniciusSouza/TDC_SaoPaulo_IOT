using iot4dx.DataObjects;
using Microsoft.ServiceBus.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace iot4dx.Helper
{
    public static class PushHelper
    {

        private static NotificationHubClient Hub { get; set; }

        public static async Task<bool> SendPush(PushMessage message)
        {

            try
            {
                Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://iot-tdc-2014hub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=YOXL/NF+JPM+VERp2d6LU95mQ0T0YfTj7C8fZ+PcXUU=", "iot-tdc-2014hub");
                                                                              

                var user = HttpContext.Current.User.Identity.Name;
                var userTag = "username:"+user;
                 

                if (message.Microsoft)
                {
                    var toast = "<toast><visual><binding template=\"ToastText01\"><text id=\"1\">" + message.Mensagem + "</text></binding></visual></toast>";
                    //Encoding Utf8 = new UTF8Encoding(false);
                    //byte[] bytes = Encoding.Default.GetBytes(toast);
                    //toast = Utf8.GetString(bytes);

                    var result = await Hub.SendMpnsNativeNotificationAsync(prepareToastPayload(message.Mensagem,""), userTag);
                }

                //if (message.Android)
                //{
                //    await Hub.SendGcmNativeNotificationAsync("{ \"data\" : {\"msg\":\"" + message.Mensagem + "\"}}");
                //}

            }
            catch (System.Exception ex)
            {
                var text = ex.Message;
                return false;
            }

            return true;
        }

        private static string prepareToastPayload(string text1, string text2)
        {
            // Create encoding manually in order to prevent
            // creation of leading BOM (Byte Order Mark) xFEFF at start
            // of string created from the XML
            Encoding Utf8 = new UTF8Encoding(false); // Prevents creation of BOM
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = false,
                //   Encoding = Encoding.UTF8    !!NO-> adds Unicode BOM to start
                Encoding = Utf8,    // Use manually created UTF8 encoding
            };
            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("wp", "Notification", "WPNotification");
            writer.WriteStartElement("wp", "Toast", "WPNotification");
            writer.WriteStartElement("wp", "Text1", "WPNotification");
            writer.WriteValue(text1);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        
    }
}