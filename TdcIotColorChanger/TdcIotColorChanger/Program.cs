using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using System.IO;
using System.Net;
using Json.NETMF;
using Microsoft.SPOT.Hardware;
using Gadgeteer.Interfaces;

namespace TdcIotColorChanger
{
    public partial class Program
    {
        string[] colors = new string[] { "red", "green", "blue", "orange" };
        Random rand = new Random((int)DateTime.Now.Ticks);

        DigitalOutput redLights;
        DigitalOutput greenLights;
        DigitalOutput blueLights;
        DigitalOutput orangeLights;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {

            redLights = extender.SetupDigitalOutput(GT.Socket.Pin.Seven, true);
            greenLights = extender.SetupDigitalOutput(GT.Socket.Pin.Eight, true);
            blueLights = extender.SetupDigitalOutput(GT.Socket.Pin.Five, true);
            orangeLights = extender.SetupDigitalOutput(GT.Socket.Pin.Six, true);

            //var t = new GT.Timer(1);
            //t.Tick += tm =>
            //    {
            //        ChangeLights(true, false, false, false);
            //        Thread.Sleep(10000);
            //        ChangeLights(false, true, false, false);
            //        Thread.Sleep(10000);
            //        ChangeLights(false, false, true, false);
            //        Thread.Sleep(10000);
            //        ChangeLights(false, false, false, true);
            //        Thread.Sleep(10000);
            //    };
            //t.Start();

            //return;
            ethernet.UseDHCP();

            //ethernet.UseStaticIP("192.168.0.3", "255.255.255.0", "192.168.0.1", new string[] { "192.168.0.1" });
            
            ethernet.NetworkUp += (sender, state) =>
                {                   
                    RequestColor();
                    var timer = new GT.Timer(10000);
                    timer.Tick += timer_Tick;
                    timer.Start();
                };

            button.ButtonReleased += (o, args) => RequestColor();
        }

        void timer_Tick(GT.Timer timer)
        {
            timer.Stop();
            RequestColor();
            timer.Start();
        }


        void RequestColor()
        {
            var color = colors[rand.Next(4)];

            try
            {
                //var request = (HttpWebRequest)WebRequest.Create("http://echo.jsontest.com/color/" + color);
                var request = (HttpWebRequest)WebRequest.Create("http://iot4dx.azurewebsites.net/api/GetNextColor");
                request.Method = "GET";
                request.KeepAlive = true;

                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        try
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                try
                                {
                                    var serializer = new JsonSerializer();
                                    var information = serializer.Deserialize(reader.ReadToEnd());
                                    ChangeColor((information as System.Collections.Hashtable)["color"].ToString());
                                }
                                catch (Exception e)
                                {
                                    Debug.Print(e.Message);
                                    Debug.Print("Error reading from stream reader");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Print(e.Message);
                            Debug.Print("Error parsing reponse after received");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Print("Could not get response...");
                    Debug.Print(e.Message);
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }

        void ChangeColor(string color)
        {
            Debug.Print(color);

            redLights.Write(false);

            switch (color)
            {
                case "red":
                    multicolorLed.TurnRed();
                    ChangeLights(true, false, false, false);
                    break;

                case "green":
                    multicolorLed.TurnGreen();
                    ChangeLights(false, true, false, false);
                    break;

                case "blue":
                    multicolorLed.TurnBlue();
                    ChangeLights(false, false, true, false);
                    break;

                case "orange":
                    multicolorLed.TurnColor(GT.Color.Orange);
                    ChangeLights(false, false, false, true);
                    break;
            }
        }

        void ChangeLights(bool red, bool green, bool blue, bool orange)
        {
            redLights.Write(!red);
            greenLights.Write(!green);
            blueLights.Write(!blue);
            orangeLights.Write(!orange);
        }
    }
}
