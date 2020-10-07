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
            await Task.Run(() =>  Speakers.Start());
            await ListeningNewSettings(); // to set new parameters while running
        }

        private static Task ListeningNewSettings()
        {
            do
            {
                try
                {
                    var line = Console.ReadLine();
                    if (line.ToLower().Contains("!setproject"))
                    {
                        Commands.ProjectLink = line.Split(" ")[1];
                        Console.WriteLine("Projet link is :" + Commands.ProjectLink);
                    }

                    if (line.ToLower().Contains("!settetris")) 
                    {
                        Commands.TetrisLink = line.Split(" ")[1];
                        Console.WriteLine("Tetris link is: " + Commands.TetrisLink);
                    }
                    line = line.ToLower();
                    if (line.ToLower().Contains("!setspeaker"))
                    {
                        switch(line.Split(" ")[1])
                        {
                            case "pause":
                                Speakers.Speaker = Status.Paused;
                                break;
                            case "play":
                            case "true":
                                Speakers.Speaker = Status.Enabled;
                                break;
                            case "false":
                                Speakers.Speaker = Status.Disabled;
                                break;
                        }
                        Console.WriteLine("Speaker now is: " + Speakers.Speaker.ToString());
                    }

                    Core.Analizer(line);
                }
                catch (Exception except)
                {
                    Console.WriteLine(except.Message);
                }
            } while (true);
        }
    }
}