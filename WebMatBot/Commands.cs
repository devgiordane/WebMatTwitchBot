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

        public static IList<Command> List = new List<Command>()
        {
            //{new Command("!Projeto" , async (string text, string user) => await Projeto(), "" )},
            //{new Command("!Tetris" , async (string text, string user) => await Tetris(), "" ) },
            {new Command("!Store" , async (string text, string user) => await Store(), "" ) },
            {new Command("!Donate" , async (string text, string user) => await Donate(), "" ) },
            {new Command("!DonatePP" , async (string text, string user) => await DonatePP(), "" ) },
            //{new Command("!Tela" , async (string text, string user) => await Screen(), "" ) },
            {new Command("!Pesquisa" , async (string text, string user) => await Pesquisa(), "" ) },
            {new Command("!Salve" , async (string text, string user) => await Salve(), "" ) },
            {new Command("!GitHub" , async (string text, string user) => await GitHub(), "" ) },
            //{new Command("!Bot" , async (string text, string user) => await GitHub(), "" ) },
            {new Command("!Exclamação" , async (string text, string user) =>  await Exclamacao(), "" )},
            {new Command("!Piada" , (string text, string user) =>  Sounds.Piada(), "" )},
            {new Command("!DeiF5" ,async (string text, string user) => await Cache.Respond(user), "" ) },
            {new Command("!Top", async(string text, string user) => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes..."), "" ) },

            {new Command("!Translate", async(string text, string user) => await Translate.TranslateText(text,true), "" ) },
            {new Command("!SetTranslate",async (string text, string user) => await AutomaticTranslator.Command(text), "" ) },
            {new Command("!SpeakTranslate", async(string text, string user) =>  await GoogleSpeakers.SpeakTranslate(text), "" )},

            {new Command("!Speak", async (string text, string user) => await SpeakerCore.QueueAdd(async () => await SpeakerCore.Speak(text)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakPt",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.pt)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEn",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.en)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakDe",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.de)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakRu",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.ru)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakFr",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.fr)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakIt",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.it)) , "" )}, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakAr",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.ar)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEl",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.el)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakJa",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.ja)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakZh",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.zh)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEs",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.es)), "" ) }, //to activate spekaer... goes to console and type "!setspeaker true

            {new Command("!ScreenVS", async (string text, string user) => await Screens.VS(), "" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenVSCode", async (string text, string user) => await Screens.VSCode(), "" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenBrowser", async (string text, string user) => await Screens.Browser(), "" ) } //to activate screen change... goes to console and type "!setscreen true"


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

        private static async Task Store()
        {
            await Core.Respond("https://streamelements.com/webmat1/store");
        }

        private static async Task Donate()
        {
            await Core.Respond("https://app.picpay.com/user/WebMat");
        }

        private static async Task DonatePP()
        {
            await Core.Respond("https://streamlabs.com/webmat1/tip");
        }

        private static async Task Screen()
        {
            await SpeakerCore.QueueAdd(async () => await SpeakerCore.Speak("Ta lindo essa tela errada..."));
        }

        private static async Task Exclamacao()
        {
            foreach(var item in List)
                await Core.Respond(item.Key);
        }

        private static async Task Pesquisa()
        {
            await Core.Respond("https://forms.gle/nzF1M8DaH1c38Pce6");
        }

    }
}
