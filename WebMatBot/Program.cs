using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;

namespace WebMatBot
{
    class Program 
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => Core.Start()); // run the core of twitch connection in a new thread
            await Task.Run(() =>  SpeakerCore.Start());
            await ListeningNewSettings(); // to set new parameters while running
        }

        private static async Task ListeningNewSettings()
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

                    
                    if (line.ToLower().Contains("!setspeaker"))
                    {
                        line = line.ToLower();
                        switch (line.Split(" ")[1])
                        {
                            case "pause":
                                SpeakerCore.Speaker = Status.Paused;
                                break;
                            case "play":
                            case "true":
                                SpeakerCore.Speaker = Status.Enabled;
                                break;
                            case "false":
                                SpeakerCore.Speaker = Status.Disabled;
                                break;
                        }
                        Console.WriteLine("Speaker now is: " + SpeakerCore.Speaker.ToString());
                    }

                    if (line.ToLower().Contains("!setscreen"))
                    {
                        switch (line.Split(" ")[1])
                        {
                            case "true":
                                Screens.isActive = true;
                                break;
                            case "false":
                                Screens.isActive = false;
                                break;
                        }
                        Console.WriteLine("Speaker now is: " + (Screens.isActive ? "Active" : "Deactivated"));
                    }

                    await Core.Analizer(line);
                }
                catch (Exception except)
                {
                    Console.WriteLine(except.Message);
                }
            } while (true);
        }
    }
}