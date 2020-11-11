using F23.StringSimilarity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebMatBot.Core;
using WindowsInput;

namespace WebMatBot
{
    public class Program 
    {
        static async Task Main(string[] args)
        {
            Task.Run(() => WebMatBot.IrcEngine.Start()); // run the core of twitch connection in a new thread
            Task.Run(() => TasksQueueOutput.Start());
            Task.Run(() => AutomaticMessages.Start());
            Task.Run(() => Lights.Light.Start());
            Task.Run(() => PubSubEngine.Start());
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
                                SpeakerCore.Status = Status.Paused;
                                break;
                            case "play":
                            case "true":
                                SpeakerCore.Status = Status.Enabled;
                                break;
                            case "false":
                                SpeakerCore.Status = Status.Disabled;
                                break;
                        }
                        Console.WriteLine("Speaker now is: " + SpeakerCore.Status.ToString());
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

                    if (line.ToLower().Contains("!settroll"))
                    {
                        switch (line.Split(" ")[1])
                        {
                            case "true":
                                Sounds.TrollisActive = true;
                                break;
                            case "false":
                                Sounds.TrollisActive = false;
                                break;
                        }
                        Console.WriteLine("Speaker Troll now is: " + (Sounds.TrollisActive ? "Active" : "Deactivated"));
                    }

                    if (line.ToLower().Contains("!setspeaktime"))
                    {
                        line = line.ToLower();
                        int newTime = TasksQueueOutput.TimeSleeping;

                        int.TryParse(line.Split(" ")[1], out newTime);

                        TasksQueueOutput.TimeSleeping = newTime;
                                
                        Console.WriteLine("Speaker now has time delay: " + TasksQueueOutput.TimeSleeping.ToString() + " seconds");
                    }

                    await WebMatBot.IrcEngine.Analizer(":"+ Parameters.User+"!"+ Parameters.User + "@"+ Parameters.User + ".tmi.twitch.tv PRIVMSG " + line);
                }
                catch (Exception except)
                {
                    Console.WriteLine(except.Message);
                }
            } while (true);
        }
    }
}