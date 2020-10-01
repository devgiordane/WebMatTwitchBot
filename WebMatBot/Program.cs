using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebMatBot
{
    class Program 
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => Core.Start()); // run the core of twitch connection in a new thread
            await ListeningNewSettings(); // to set new parameters while running
        }

        private static Task ListeningNewSettings()
        {
            do
            {
                try
                {
                    var line = Console.ReadLine();

                    if (line.ToLower().Contains("!setproject")) Commands.ProjectLink = line.Split(" ")[1];
                    if (line.ToLower().Contains("!settetris")) Commands.TetrisLink = line.Split(" ")[1];
                    if (line.ToLower().Contains("!setspeaker")) Commands.Speaker = bool.Parse(line.Split(" ")[1]);

                }
                catch (Exception except)
                {
                    Console.WriteLine(except.Message);
                }
            } while (true);
        }
    }
}