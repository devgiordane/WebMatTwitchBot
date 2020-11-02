using F23.StringSimilarity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebMatBot
{
    public static class Engine
    {
        private static ClientWebSocket webSocket { get; set; }

        //all words in lowercase
        private static List<string> badWords = new List<string> { "mongoloide","mongolóide","mongol","pinto", "buceta", "toma no cu", "tomar no cu" };

        public static async void Start()
        {
            do
            {
                using (var socket = new ClientWebSocket())
                    try
                    {
                        await socket.ConnectAsync(new Uri("wss://irc-ws.chat.twitch.tv:443"), CancellationToken.None);

                        webSocket = socket;

                        await Send("PASS " + Parameters.OAuth, CancellationToken.None);
                        await Send("NICK " + Parameters.User, CancellationToken.None);
                        await Send("JOIN #" + Parameters.User, CancellationToken.None);

                        await Respond("Estou conectado... Muito bom estar aqui com vcs...");

                        await Receive(CancellationToken.None);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR - {ex.Message}");
                    }
            } while (true);
        }

        public static async Task Send(string data, CancellationToken stoppingToken) =>
        await webSocket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, stoppingToken);

        public static async Task Receive(CancellationToken stoppingToken)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            while (!stoppingToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await webSocket.ReceiveAsync(buffer, stoppingToken);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        var input = await reader.ReadToEndAsync();
                        //Console.WriteLine(input);
                        if (await Analizer(input))
                        {
                            Cache.AddToCacheMessage(input);
                        }
                    }
                }
            };
        }

        public static async Task Respond(string msg)
        {
            try
            {
                await Send("PRIVMSG #" + Parameters.User + " : MrDestructoid " + msg, CancellationToken.None);
            }
            catch(Exception except)
            {
                Console.WriteLine(except.Message);
            }
        }

        public static async Task Whisper(string user, string msg)
        {
            await Send(@"PRIVMSG #" + Parameters.User + " :/w " + user + " " + msg, CancellationToken.None);
        }

        public static async Task<bool> Analizer(string input)
        {
            //must responde ping pong
            if (input.Contains("PING")) 
            { 
                await Send("PONG", CancellationToken.None);
                return false; 
            }

            var filter = input.ToLower();
            if (badWords.Any(s => filter.Contains(s)))
            {
                await Respond("Sua Mensagem contém palavras impróprias e não será repassada ao nosso bot!");
                await Send(@"PRIVMSG #" + Parameters.User + " :/timeout "+ input.Split("!")[0].Replace(":","") +" 1m", CancellationToken.None);
                return false;
            }

            //check all counters and increase if necessary
            Counters.CheckCounter(input);

            await CheckCommand(input);

            Console.WriteLine(input);

            return true;
        }

        private static async Task CheckCommand(string input)
        {
            var words = input.ToLower().Split(" ");
            Command command = null;
            bool isDone = false;

            // verifica comandos
            for (int i = 0; i< Commands.List.Count() && !isDone;  i++)
            {
                if (words.Any(q => q.Trim().Replace(":", "").Equals(Commands.List[i].Key.ToLower())))
                {
                    command = Commands.List[i];
                    isDone = true;
                }
            }

            if (isDone)
            {
                //limite de caracteres
                if (input.Length > 550)
                {
                    await Respond("Sua Mensagem contém muitos caracteres e não será repassada ao nosso bot!");
                    return;
                }
                else
                {
                    command.Action.Invoke(input.ToLower().Split(command.Key.ToLower())[1], input.Split("!")[0].Replace(":", ""));
                }
            }
            else if (words.Length >= 4 && words[3].StartsWith(":!"))
            {
                await CommandCorrector(input, words[3].Replace(":", ""));
            }
            
        }

        public static async Task CommandCorrector(string input, string command, string user = null,bool shouldBeExact =false)
        {
            // def variables
            IDictionary<Command ,double> MatchRate = new Dictionary<Command, double>();
            var Match = new NormalizedLevenshtein();

            // filling array with matching rate
            for (int i = 0; i < Commands.List.Count; i++)
                MatchRate.Add(Commands.List[i], Match.Distance(command, Commands.List[i].Key));

            //is there some rate lower than 35%
            if (!MatchRate.Any(q => q.Value <= 0.51d))
            {
                await Respond("@" + input.ToLower().Split("!")[0].Replace(":", "") + " , Não entendi o seu comando, tente !Exclamação para obter a lista de todos os comandos...");
                return;
            }
            else
            {
                //get the minimum match rate (closest command)
                var minimum = MatchRate.Min(q=>q.Value);

                var arrayMinimum = MatchRate.Where(q => q.Value == minimum);

                if (arrayMinimum.Count() == 1)
                {
                    if (shouldBeExact)
                    {
                        await Respond("@" + user + " , O comando " + command + " está incorreto; " + arrayMinimum.ElementAt(0).Key.Description);
                    }
                    else
                    {
                        var Tinput = input.ToLower().Split(command)[1];
                        var Tuser = input.ToLower().Split("!")[0].Replace(":", "");

                        await Respond("@" + Tuser + " , Seu commando foi corrigido para " + arrayMinimum.ElementAt(0).Key.Key + ", tente !Exclamação para obter a lista de todos os comandos...");

                        arrayMinimum.ElementAt(0).Key.Action.Invoke(Tinput, Tuser);
                    }
                    
                }
                else
                {
                    string text = "@" + input.ToLower().Split("!")[0].Replace(":", "") + " , Não entendi o seu comando, não seria ";
                    foreach(var item in arrayMinimum)
                    {
                        text += item.Key.Key  + " ou ";
                    }

                    text += "tente !Exclamação para ver todos os comandos...";

                    await Respond(text);
                }
            }
        }
    }
}
