using iot4dx.DataObjects;
using iot4dx.Models;
using iot4dx.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Twitterizer;

namespace iot4dx
{
    public class GetNextColorController : ApiController
    {
        private MobileServiceDBContext db = new MobileServiceDBContext();

        private static DataObjects.MicrosoftColors LastColor = MicrosoftColors.Blue;
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            db = new MobileServiceDBContext();

        }

        // GET api/GetNextColor
        /// <summary>
        /// Retorna a próxima cor a ser alterada no Stand
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {

            var iotMessageQuery = db.IoTActivities.Where<IoTActivity>(iot => iot.Complete == false).OrderBy(iot => iot.CreatedAt);
            StringBuilder sb = new StringBuilder();



            if (iotMessageQuery.Count() > 0)
            {
                var iotMessage = iotMessageQuery.First();

                if (iotMessage != null)
                {
                    GetNextColorController.LastColor = iotMessage.Color;

                    iotMessage.Complete = true;
                    db.SaveChangesAsync();

                    sb.Append(String.Format("{0}\"{1}\":\"{2}\"{3}", "{", "color", iotMessage.Color.ToString().ToLower(), "}"));

                    iot4dx.Twitter.TwitterHelper.TwitteStatus(string.Format("User: {0} - Color: {1}",  iotMessage.User, iotMessage.Color.ToString()));

                }
                else
                {
                    sb.Append(String.Format("{0}\"{1}\":\"{2}\"{3}", "{", "color", GetNextColorController.LastColor.ToString().ToLower(), "}"));

                    iot4dx.Twitter.TwitterHelper.TwitteStatus(string.Format("Last Color used - Color: {0}", iotMessage.Color.ToString()));

                }
            }
            else
            {
                

                sb.Append(String.Format("{0}\"{1}\":\"{2}\"{3}", "{", "color", GetNextColorController.LastColor.ToString().ToLower(), "}"));
                
                
                //Para teste utilizando cores Randômicas
                //var rand = new Random(DateTime.Now.Millisecond);
                //DataObjects.MicrosoftColors Color = (DataObjects.MicrosoftColors)rand.Next(1, 5);
                //sb.Append(String.Format("{0}\"{1}\":\"{2}\"{3}", "{", "color", Color.ToString().ToLower(), "}"));

               //Envio de twitte na conta
               // iot4dx.Twitter.TwitterHelper.TwitteStatus(string.Format("Modo de Teste IoT Color: {0}", Color.ToString()));

            }

            
            var resp =new HttpResponseMessage(){
                Content = new StringContent(sb.ToString())
            };

            return resp;

        }

    }
}