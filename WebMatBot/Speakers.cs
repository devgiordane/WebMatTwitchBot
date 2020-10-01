using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebMatBot
{
    public class Speakers
    {
        private static SpeechConfig config = (string.IsNullOrEmpty(Parameters.AzureCognitiveKey)||string.IsNullOrEmpty(Parameters.AzureCognitiveRegion)) ? null : SpeechConfig.FromSubscription(Parameters.AzureCognitiveKey, Parameters.AzureCognitiveRegion);//https://portal.azure.com/
        public static bool Speaker { get; set; } = false;

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

            config.SpeechSynthesisVoiceName = "pt-PT-HeliaRUS";
            using var synthesizer = new SpeechSynthesizer(config);

            var ssml = File.ReadAllText("SSML.xml").Replace("{text}", textToSpeech);
            var result = await synthesizer.SpeakSsmlAsync(ssml);

            //var teste = await synthesizer.SpeakTextAsync(textToSpeech+".ora pois.");
        }

        private static async Task<bool> CheckStatus()
        {
            if (!Speaker)
            {
                await Core.Respond("O Speaker está off... peça o streamer para aciona-lo...");
                return false;
            }
            else
                return true;
        }
    }
}
