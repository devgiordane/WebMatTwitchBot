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
        
        public static IDictionary<string, Action<string>> List = new Dictionary<string, Action<string>>()
        {
            {"!Projeto" , async (string text) => await Projeto() },
            {"!Duelo" , async (string text) => await Duelo() },
            {"!Tetris" , async (string text) => await Tetris() },
            {"!Pesquisa" , async (string text) => await Pesquisa() },
            {"!Salve" , async (string text) => await Salve() },
            {"!Caiu" , async (string text) => await Caiu() },
            {"!GitHub" , async (string text) => await GitHub() },
            {"!Bot" , async (string text) => await GitHub() },
            {"!Exclamação" , async (string text) =>  await Exclamacao()},
            {"!DeiF5" ,async (string text) => await Cache.Respond() },
            {"!Top", async(string text) => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes...") },
            {"!Speak", async (string text) => await Speakers.QueueAdd(async () => await Speakers.Speak(text)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakPt",async (string text) => await Speakers.QueueAdd(async () => await Speakers.SpeakPortuga(text)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakEn",async (string text) => await Speakers.QueueAdd(async () => await Speakers.SpeakEnglish(text)) } //to activate spekaer... goes to console and type "!setspeaker true"
        };

        private static async Task Projeto()
        {
            await Core.Respond(ProjectLink);
        }

        private static async Task GitHub()
        {
            await Core.Respond("https://github.com/WebMat1/WebMatTwitchBot");
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
