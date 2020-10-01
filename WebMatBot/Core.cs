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
    public static class Core
    {
        private static ClientWebSocket webSocket { get; set; }

        //all words in lowercase
        private static List<string> badWords = new List<string> { "mongoloide","mongolóide","mongo","pinto", "buceta", "toma no cu", "tomar no cu" };

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
                        Console.WriteLine(input);
                        Analizer(input);
                        Cache.AddToCacheMessage(input);
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

        public static async void Analizer(string input)
        {
            //must responde ping pong
            if (input.Contains("PING")) 
            { 
                await Send("PONG", CancellationToken.None);
                return; 
            }

            var filter = input.ToLower();
            if (badWords.Any(s => filter.Contains(s)))
            {
                await Respond("Sua Mensagem contém palavras impróprias e não será repassada ao nosso bot!");
                await Send(@"PRIVMSG #" + Parameters.User + " :/timeout "+ input.Split("!")[0].Replace(":","") +" 1m", CancellationToken.None);
                return;
            }
            //check all counters and increase if necessary
            Counters.CheckCounter(input);


            var words = input.ToLower().Split(" ");
            // verifica comandos
            foreach (var cmd in Commands.List)
            {
                if (words.Any(q => q.Trim().Replace(":", "").Equals(cmd.Key.ToLower())))
                    cmd.Value.Invoke(input.ToLower().Split(cmd.Key.ToLower())[1]);
            }
        }
    }
}
