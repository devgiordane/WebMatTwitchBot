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
            {"!Tetris" , async (string text) => await Tetris() },
            {"!Store" , async (string text) => await Store() },
            {"!Donate" , async (string text) => await Donate() },
            {"!DonatePP" , async (string text) => await DonatePP() },
            {"!Tela" , async (string text) => await Screen() },
            {"!Pesquisa" , async (string text) => await Pesquisa() },
            {"!Salve" , async (string text) => await Salve() },
            {"!GitHub" , async (string text) => await GitHub() },
            {"!Bot" , async (string text) => await GitHub() },
            {"!Exclamação" , async (string text) =>  await Exclamacao()},
            {"!DeiF5" ,async (string text) => await Cache.Respond() },
            {"!Top", async(string text) => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes...") },

            {"!Translate", async(string text) => await Translate.TranslateText(text,true) },
            {"!SetTranslate",async (string text) => await AutomaticTranslator.Command(text) },
            {"!SpeakTranslate", async(string text) =>  await GoogleSpeakers.SpeakTranslate(text)},

            {"!Speak", async (string text) => await SpeakerCore.QueueAdd(async () => await SpeakerCore.Speak(text)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakPt",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.pt)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakEn",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.en)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakDe",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.de)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakRu",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.ru)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakFr",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.fr)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakIt",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.it)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakAr",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.ar)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakEl",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.el)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakJa",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.ja)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakZh",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.zh)) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {"!SpeakEs",async (string text) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, Translate.Languages.es)) } //to activate spekaer... goes to console and type "!setspeaker true"
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
