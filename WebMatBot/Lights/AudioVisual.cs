using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI.Models.ColorFlow;

namespace WebMatBot.Lights
{
    public class AudioVisual
    {
        public static async Task Party(string txt, string user)
        {
            try
            {
                //sem await para não ter q parar as luzes e depois falar o texto
                Light.StartLightFlow();

                Sounds.RandomPartySound();

                await Light.StopLightFlow();
                    
            }
            catch (Exception ex)
            {
                await IrcEngine.CommandCorrector(txt, "!Light", user, true);
            }
        }

        

        public static async Task Xandao(string txt, string user)
        {
            try
            {
                //sem await para não ter q parar as luzes e depois falar o texto
                Light.StartLightFlow();

                Sounds.Xandao();

                await Light.StopLightFlow();

            }
            catch (Exception ex)
            {
                await IrcEngine.CommandCorrector(txt, "!AudioVisual", user, true);
            }
        }
    }
}
