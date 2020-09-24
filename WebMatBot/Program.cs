using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebMatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => Start());
            Console.ReadLine();
        }

        private static async void Start()
        {
            do
            {
                using (var socket = new ClientWebSocket())
                try
                {
                    await socket.ConnectAsync(new Uri("wss://irc-ws.chat.twitch.tv:443"), CancellationToken.None);

                    await Send(socket, "PASS " + Parameters.OAuth, CancellationToken.None);
                    await Send(socket, "NICK "+ Parameters.User, CancellationToken.None);
                    await Send(socket, "JOIN #"+ Parameters.User, CancellationToken.None);

                    await Send(socket, "PRIVMSG #webmat1 : half command", CancellationToken.None);

                    await Receive(socket, CancellationToken.None);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR - {ex.Message}");
                }
            } while (true);
        }

        private static async Task Send(ClientWebSocket socket, string data, CancellationToken stoppingToken) =>
        await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, stoppingToken);

        private static async Task Receive(ClientWebSocket socket, CancellationToken stoppingToken)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            while (!stoppingToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await socket.ReceiveAsync(buffer, stoppingToken);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                        Console.WriteLine(await reader.ReadToEndAsync());
                }

            };
        }
    }
}
