using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMatBot.Core;

namespace WebMatBot
{
    public static class Commands
    {
        public static string ProjectLink { get; set; }
        public static string TetrisLink { get; set; }

        public static IList<Command> List = new List<Command>()
        {
            {new Command("!Tetris" , async (string text, string user) => await Tetris(), "" ) },
            {new Command("!Store" , async (string text, string user) => await Store(), "" ) },
            {new Command("!Donate" , async (string text, string user) => await Donate(), "" ) },
            {new Command("!DonatePP" , async (string text, string user) => await DonatePP(), "" ) },

            {new Command("!Pesquisa" , async (string text, string user) => await Pesquisa(), "" ) },
            {new Command("!Salve" , async (string text, string user) => await Salve(), "" ) },
            {new Command("!GitHub" , async (string text, string user) => await GitHub(), "" ) },
            {new Command("!YouTube" , async (string text, string user) => await Youtube(), "" ) },
            {new Command("!Discord" , async (string text, string user) => await Discord(), "" ) },

            {new Command("!Exclamação" , async (string text, string user) =>  await Exclamacao(user), "" )},
            {new Command("!Piada" , (string text, string user) =>  Sounds.Piada(), "" )},
            {new Command("!Clap" , (string text, string user) =>  Sounds.Clap(), "" )},
            {new Command("!DeiF5" ,async (string text, string user) => await Cache.Respond(user), "" ) },
            {new Command("!Top", async(string text, string user) => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes..."), "" ) },

            {new Command("!Translate", async(string text, string user) => await Translate.TranslateText(text,true), "" ) },
            {new Command("!SetTranslate",async (string text, string user) => await AutomaticTranslator.Command(text), "" ) },
            {new Command("!SpeakTranslate", async(string text, string user) =>  await GoogleSpeakers.SpeakTranslate(text, user), "" )},

            {new Command("!Speak", async (string text, string user) => await SpeakerCore.QueueAdd(async () => await SpeakerCore.Speak(text, user)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakPt",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.pt)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEn",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.en)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakDe",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.de)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakRu",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.ru)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakFr",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.fr)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakIt",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.it)) , "" )}, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakAr",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.ar)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEl",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.el)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakJa",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.ja)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakZh",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.zh)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEs",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.es)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true

            {new Command("!ScreenVS", async (string text, string user) => await Screens.VS(), "" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenVSCode", async (string text, string user) => await Screens.VSCode(), "" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenCozinha", async (string text, string user) => await Screens.Kitchen(), "" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenBrowser", async (string text, string user) => await Screens.Browser(), "" ) } //to activate screen change... goes to console and type "!setscreen true"


        };

        private static async Task Youtube()
        {
            await Engine.Respond(StreamerDefault.Youtube);
        }

        private static async Task Discord()
        {
            await Engine.Respond(StreamerDefault.Discord);
        }

        private static async Task GitHub()
        {
            await Engine.Respond(StreamerDefault.GitHub);
        }

        private static async Task Tetris()
        {
            await Engine.Respond(TetrisLink);
        }

        private static async Task Salve()
        {
            await Engine.Respond("Salve alá Igão... FortBush CurseLit");
        }

        private static async Task Store()
        {
            await Engine.Respond(StreamerDefault.StreamElements_Store);
        }

        private static async Task Donate()
        {
            await Engine.Respond(StreamerDefault.PicPay);
        }

        private static async Task DonatePP()
        {
            await Engine.Respond(StreamerDefault.PayPal);
        }

        private static async Task Exclamacao(string user)
        {
            foreach(var item in List)
                await Engine.Respond(item.Key);

            //await Main.Respond(user + ", confira a aba de sussurros...");
        }

        private static async Task Pesquisa()
        {
            await Engine.Respond(StreamerDefault.Form);
        }

    }
}
