using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMatBot.Core;
using WebMatBot.Lights;

namespace WebMatBot
{
    public static class Commands
    {
        public static string ProjectLink { get; set; }
        public static string TetrisLink { get; set; }

        public static IList<Command> List = new List<Command>()
        {
            {new Command("!Tetris" , async (string text, string user) => await Tetris(), "Quando estivermos jogando tetris, envie esse comando para receber o link da sala e jogar conosco;" ) },
            {new Command("!Store" , async (string text, string user) => await Store(), "Para ver itens na nossa loja da streamelements, ou até mesmo utilizar a voz do Ricardo (9? 99? G? 3? X?);" ) },
            {new Command("!Donate" , async (string text, string user) => await Donate(), "Apoie nosso canal financeiramente... Muito obrigado! Namastê! Link do PicPay;" ) },
            {new Command("!DonatePP" , async (string text, string user) => await DonatePP(), "Apoie nosso canal financeiramente... Muito obrigado! Namastê! Link do PayPal;"  ) },

            {new Command("!Pesquisa" , async (string text, string user) => await Pesquisa(), "Deixe-me entender mais sobre o que você gostaria de ver na live;" ) },
            {new Command("!Salve" , async (string text, string user) => await Salve(), "Cumprimento Alah Igão;" ) },
            {new Command("!GitHub" , async (string text, string user) => await GitHub(), "Caso tenha curiosidade de entender mais sobre esse que vos fala (nosso bot);" ) },
            {new Command("!YouTube" , async (string text, string user) => await Youtube(), "Compilados do que há de super interessante nessa live, divirta-se;" ) },
            {new Command("!Discord" , async (string text, string user) => await Discord(), "Aqui a comunidade é forte e parceira, junte-se a nós;" ) },

            {new Command("!Exclamação" , async (string text, string user) =>  await Exclamacao(user), "Lista todos os comandos disponíveis aqui;" )},
            {new Command("!Piada" , (string text, string user) =>  Sounds.Piada(), "Faz o nosso bot tocar aquele som de fim de piada (bateria);" )},
            {new Command("!Clap" , (string text, string user) =>  Sounds.Clap(), "Faz o nosso bot tocar som de palmas, pra exaltar o streamer;" )},
            {new Command("!DeiF5" ,async (string text, string user) => await Cache.Respond(user), "Caso sua live tenha travado e você recarregar a pagina, use esse comando para obter as ultimas mensagens enviadas no nosso chat;" ) },
            {new Command("!Top", async(string text, string user) => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes..."), "O streamer tem o vício de falar Top... e você pode ajudar a contar quantas vezes ele faz isso;" ) },

            {new Command("!Translate", async(string text, string user) => await Translate.TranslateText(text,true), "Traduz um texto para linguagem desejada : !Translate {sigla da lingua de origem}-{sigla da lingua de destino} {texto a ser traduzido} : !Translate en-pt Hello World! ;" ) },
            {new Command("!SetTranslate",async (string text, string user) => await AutomaticTranslator.Command(text), "Todo texto enviado nos comandos de Speak são traduzidos para a lingua desejada e enviados automaticamente no chat : !SetTranslate {lingua desejada} : !SetTranslate pt ;" ) },
            {new Command("!SpeakTranslate", async(string text, string user) =>  await GoogleSpeakers.SpeakTranslate(text, user), "Se você quiser dizer algo em outra lingua mas não sabe como se escreve, tente isso : !SpeakTranslate {lingua que a voz irá falar} {texto para a voz falar} : !speaktranslate en Olá a todos lindões;" )},

            {new Command("!Speak", async (string text, string user) => await SpeakerCore.QueueAdd(async () => await SpeakerCore.Speak(text, user)), "Fala com lingua em pt-br : !speak {seu texto aqui} : !speak Falo do brasil..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakPt",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.pt)), "Fala com lingua em pt-pt : !speakpt {seu texto aqui} : !speakpt falo de Portugal..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEn",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.en)), "Fala com lingua em english : !speaken {seu texto aqui} : !speaken falo em Inglês..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakDe",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.de)), "Fala com lingua em alemão : !speakde {seu texto aqui} : !speakde falo em Alemão..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakRu",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.ru)), "Fala com lingua em russo : !speakru {seu texto aqui} : !speakru falo em Russo (cabeça)..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakFr",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.fr)), "Fala com lingua em francês : !speakfr {seu texto aqui} : !speakfr falo em francês (we we)..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakIt",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.it)) , "Fala com lingua em italiano : !speakit {seu texto aqui} : !speakit falo em Italiano (nono nona)..." )}, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakAr",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.ar)), "Fala com lingua em árabe : !speakar {seu texto aqui} : !speakar falo em Árabe..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEl",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.el)), "Fala com lingua em grego : !speakel {seu texto aqui} : !speakel falo em Grego..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakJa",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.ja)), "Fala com lingua em japones : !speakja {seu texto aqui} : !speakja falo em Japones (uéb mat san)..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakZh",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.zh)), "Fala com lingua em chinês : !speakzh {seu texto aqui} : !speakzh falo em Chinês..." ) }, //to activate spekaer... goes to console and type "!setspeaker true"
            {new Command("!SpeakEs",async (string text, string user) => await SpeakerCore.QueueAdd(async () => await GoogleSpeakers.Speak(text, user, Translate.Languages.es)), "Fala com lingua em espanhol : !speakes {seu texto aqui} : !speakes falo em Espanhol..." ) }, //to activate spekaer... goes to console and type "!setspeaker true

            {new Command("!ScreenVS", async (string text, string user) => await Screens.VS(), "Muda a tela para ver o Visual Studio" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenVSCode", async (string text, string user) => await Screens.VSCode(), "Muda a tela para ver o Visual Studio CODE" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenCozinha", async (string text, string user) => await Screens.Kitchen(), "Muda a tela para ver a cozinha... (Nem sempre habilitado... Privacidade né...)" ) }, //to activate screen change... goes to console and type "!setscreen true"
            {new Command("!ScreenTela1", async (string text, string user) => await Screens.Browser(), "Muda a tela para ver o que há no monitor 1" ) }, //to activate screen change... goes to console and type "!setscreen true"


            {new Command("!Light", async (string text, string user) => await Light.Command(text, user), "Muda a cor da lâmpada no quarto : !light rgb({0:255} ,{0:255}, {0:255})  : !light rgb(255,255,255)" ) },

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
            {
                await Engine.Whisper(user, item.Key + "    : " + item.Description);
                await Task.Delay(750);
            }
                

            await Engine.Respond(user + ", confira a aba de sussurros...");
        }

        private static async Task Pesquisa()
        {
            await Engine.Respond(StreamerDefault.Form);
        }

    }
}
