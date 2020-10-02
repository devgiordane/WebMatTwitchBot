using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMatBot
{
    public class Speakers
    {
        private static SpeechConfig config = (string.IsNullOrEmpty(Parameters.AzureCognitiveKey)||string.IsNullOrEmpty(Parameters.AzureCognitiveRegion)) ? null : SpeechConfig.FromSubscription(Parameters.AzureCognitiveKey, Parameters.AzureCognitiveRegion);//https://portal.azure.com/
        //public static bool Speaker { get; set; } = false;
        public static Status Speaker { get; set; } = Status.Disabled;

        private static IList<Action> Queue = new List<Action>();

        public static async Task Speak(string textToSpeech, bool wait = false)
        {
            if (!await CheckStatus()) return;

            // Command to execute PS  
            Execute($@"Add-Type -AssemblyName System.speech;  
            $speak = New-Object System.Speech.Synthesis.SpeechSynthesizer;                           
            $speak.Speak(""{textToSpeech}"");"); // Embedd text  

            void Execute(string command)
            {
                // create a temp file with .ps1 extension  
                var cFile = System.IO.Path.GetTempPath() + Guid.NewGuid() + ".ps1";

                //Write the .ps1  
                using var tw = new System.IO.StreamWriter(cFile, false, Encoding.UTF8);
                tw.Write(command);

                // Setup the PS  
                var start =
                    new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = "C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe",  // CHUPA MICROSOFT 02-10-2019 23:45                    
                        LoadUserProfile = false,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"-executionpolicy bypass -File {cFile}",
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                    };

                //Init the Process  
                var p = System.Diagnostics.Process.Start(start);
                // The wait may not work! :(  
                if (wait) p.WaitForExit();
            }
        }

        public static async Task SpeakPortuga(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            var voice = "pt-PT-HeliaRUS";
            var vicio = "Ora Pois?";
            config.SpeechSynthesisVoiceName = voice;
            using var synthesizer = new SpeechSynthesizer(config);

            var ssml = File.ReadAllText("SSML.xml").Replace("{text}", textToSpeech).Replace("{voice}",voice).Replace("{posmsg}", vicio);
            var result = await synthesizer.SpeakSsmlAsync(ssml);

            //var teste = await synthesizer.SpeakTextAsync(textToSpeech+".ora pois.");
        }

        public static async Task SpeakEnglish(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            var voice = "en-AU-Catherine";
            var vicio = "You know?";
            config.SpeechSynthesisVoiceName = voice;
            using var synthesizer = new SpeechSynthesizer(config);

            var ssml = File.ReadAllText("SSML.xml").Replace("{text}", textToSpeech).Replace("{voice}", voice).Replace("{posmsg}", vicio);
            var result = await synthesizer.SpeakSsmlAsync(ssml);

            //var teste = await synthesizer.SpeakTextAsync(textToSpeech+".ora pois.");
        }

        public static Task QueueAdd (Action action)
        {
            if (Speaker != Status.Disabled)
                lock (Queue)
                    Queue.Add(action);

            return Task.CompletedTask;
        }

        public static async void Start()
        {
            do
            {
                try
                {
                    if (Speaker == Status.Disabled || Speaker == Status.Paused)
                        await Task.Delay(5000);
                    else
                    {
                        Task scoped = null;

                        lock (Queue)
                        {
                            if (Queue.Count > 0)
                            {
                                scoped = new Task(Queue[0]);
                                Queue.Remove(Queue[0]);
                            }
                        }

                        if (scoped != null)
                        {
                            scoped.Start();
                            scoped.Wait();
                        }

                        await Task.Delay(10000);
                    }
                }catch(Exception excpt)
                {
                    Console.WriteLine(excpt.Message) ;
                }
            } while (true) ;
        }

        private static async Task<bool> CheckStatus()
        {
            if (Speaker == Status.Disabled)
            {
                await Core.Respond("O Speaker está off... peça o streamer para aciona-lo...");
                return false;
            }
            else
                return true;
        }
    }

    public enum Status
    {
        Disabled,
        Enabled,
        Paused,
    }
}
