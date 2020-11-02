using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;
using YeelightAPI.Models.ColorFlow;

namespace WebMatBot.Lights
{
    public class Light
    {
        private static string ipLight = "192.168.0.28";
        private static Device device { get; set; } = new Device(ipLight, autoConnect: true);

        public static async Task Start()
        {
            try
            {
                //await device.Connect();

                //if (device != null)
                //{
                //    ColorFlow flow = new ColorFlow(0, ColorFlowEndAction.Restore);
                //    flow.Add(new ColorFlowRGBExpression(255, 0, 0, 1, 500)); // color : red / brightness : 1% / duration : 500
                //    flow.Add(new ColorFlowRGBExpression(0, 255, 0, 100, 500)); // color : green / brightness : 100% / duration : 500
                //    flow.Add(new ColorFlowRGBExpression(0, 0, 255, 50, 500)); // color : blue / brightness : 50% / duration : 500
                //    flow.Add(new ColorFlowSleepExpression(2000)); // sleeps for 2 seconds
                //    flow.Add(new ColorFlowTemperatureExpression(2700, 100, 500)); // color temperature : 2700k / brightness : 100 / duration : 500
                //    flow.Add(new ColorFlowTemperatureExpression(5000, 1, 500)); // color temperature : 5000k / brightness : 100 / duration : 500

                //    await device.StartColorFlow(flow); // start
                //}
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public static async Task Command(string cmd, string user)
        {
            cmd = cmd.Trim().Replace("\r\n", "");
            if (cmd.StartsWith("rgb"))
                await SetRGBColor(cmd, user);

            switch (cmd)
            {
                case "blue":
                    await SetRGBColor("rgb(0,0,255)", user);
                    break;
                case "red":
                    await SetRGBColor("rgb(255,0,0)", user);
                    break;
                case "green":
                    await SetRGBColor("rgb(0,255,0)", user);
                    break;
                case "yellow":
                    await SetRGBColor("rgb(255,255,0)", user);
                    break;
                case "white":
                    await SetRGBColor("rgb(255,255,255)", user);
                    break;
                case "pink":
                    await SetRGBColor("rgb(255,0,155)", user);
                    break;
                case "orange":
                    await SetRGBColor("rgb(255,155,0)", user);
                    break;

            }
        }

        private static async Task SetRGBColor(string rawSTR, string user)
        {
            try
            {
                if (!device.IsConnected)
                    return;

                //trabalhar a string para remover dados
                string[] rgb = rawSTR.Split("rgb")[1].Replace("(", "").Replace(")", "").Split(",");
                int r = int.Parse(rgb[0]);
                int g = int.Parse(rgb[1]);
                int b = int.Parse(rgb[2]);

                if (r > 255 || r < 0 || g > 255 || g < 0 || b > 255 || b < 0 || (r == 0 && g == 0 && b == 0))
                    throw new Exception("Parametro(s) inválido(s)");

                await device.SetRGBColor(r, g, b);
            }
            catch (Exception ex)
            {
                await Engine.CommandCorrector(rawSTR, "!Light", user, true);
            }
        }
    }
}
