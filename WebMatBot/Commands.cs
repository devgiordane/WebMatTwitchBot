using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebMatBot
{
    public static class Commands
    {
        public static string ProjectLink { get; set; }
        public static string TetrisLink { get; set; }

        public static IDictionary<string, Action> List = new Dictionary<string, Action>()
        {
            {"!Projeto" , async () => await Projeto() },
            {"!Duelo" , async () => await Duelo() },
            {"!Tetris" , async () => await Tetris() },
            {"!Pesquisa" , async () => await Pesquisa() },
            {"!Salve" , async () => await Salve() },
            {"!Caiu" , async () => await Caiu() },
            {"!Exclamação" , async () =>  await Exclamacao()},
            {"!DeiF5" ,async () => await Cache.Respond() },
            {"!Top", async() => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes...") }
        };

        private static async Task Projeto()
        {
            await Core.Respond(ProjectLink);
        }

        private static async Task Tetris()
        {
            await Core.Respond(TetrisLink);
        }

        private static async Task Salve()
        {
            await Core.Respond("Salve alá Igão... FortBush CurseLit");
        }

        private static async Task Caiu()
        {
            await Core.Respond("Keep Calm... just clik it: www.twitch.tv/webmat1" );
        }

        private static async Task Exclamacao()
        {
            foreach(var item in List)
                await Core.Respond(item.Key);
        }

        private static async Task Duelo()
        {
            await Core.Respond("Tá marcado... Dia 02/10/2020... às 16h... o nosso duelo de TETRIS entre FredFp e WebMat... Fisica vs Programação... O perdedor dará um sub de presente pro ganhador... Torce pra mim hein... birlll!!! ");
            await Core.Respond("So... its Scheduled... 02/10/2020... at 4 PM... The TETRIS duel between WebMat and FredFp... Its mean Physics vs Programming... The loser will give a  sub gift to winner... so... pls cheering me... ");
        }

        private static async Task Pesquisa()
        {
            await Core.Respond("https://forms.gle/nzF1M8DaH1c38Pce6");
        }
        
    }
}
