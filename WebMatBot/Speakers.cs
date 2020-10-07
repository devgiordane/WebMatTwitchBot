using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebMatBot.Translate;

namespace WebMatBot
{
    public class Speakers
    {
        private static SpeechConfig config = (string.IsNullOrEmpty(Parameters.AzureCognitiveKey)||string.IsNullOrEmpty(Parameters.AzureCognitiveRegion)) ? null : SpeechConfig.FromSubscription(Parameters.AzureCognitiveKey, Parameters.AzureCognitiveRegion);//https://portal.azure.com/
        //public static bool Speaker { get; set; } = false;
        public static Status Speaker { get; set; } = Status.Disabled;

        private static IList<Func<Task>> Queue = new List<Func<Task>>();

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

            Speaker spk = new Speaker()
            {
                Alert = "Ora Pois",
                Voice = "pt-PT-HeliaRUS",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakEnglish(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Heyyy.",
                Voice = "en-AU-Catherine",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakGerman(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Schweinsteiger",
                Voice = "de-DE-Stefan",
                Diction = ""
            };


            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakRussian(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Sputinik",
                Voice = "ru-RU-Irina",
                Diction = ""
            };


            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakFrench(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Thierry Henry",
                Voice = "fr-FR-Julie",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakArabic(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Insha'Allah",
                Voice = "ar-EG-Hoda",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakItalian(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Mama mia Marcello.",
                Voice = "it-IT-Cosimo",
                Diction = ""
            };


            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakGreece(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Malaká",
                Voice = "el-GR-Stefanos",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakJapanese(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "Naniiii",
                Voice = "ja-JP-Ichiro",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakChinese(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "wǒ shì bā xī rén",
                Voice = "zh-CN-Yaoyao",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        public static async Task SpeakSpanish(string textToSpeech)
        {
            if (!await CheckStatus() || config == null) return;

            Speaker spk = new Speaker()
            {
                Alert = "A buenas horas mangas verdes",
                Voice = "es-MX-Raul",
                Diction = ""
            };

            await SpeakAzure(spk, textToSpeech);
        }

        private static async Task SpeakAzure(ISpeaker speaker, string textToSpeech)
        {
            config.SpeechSynthesisVoiceName = speaker.Voice;
            using var synthesizer = new SpeechSynthesizer(config);

            var ssml = File.ReadAllText("SSML.xml").Replace("{text}", textToSpeech).Replace("{voice}", speaker.Voice).Replace("{posmsg}", speaker.Diction).Replace("{alert}", speaker.Alert);
            var result = await synthesizer.SpeakSsmlAsync(ssml);

            await AutomaticTranslator.Translate(textToSpeech);
        }

        public static async Task QueueAdd (Func<Task> action)
        {
            if (await CheckStatus())
                lock (Queue)
                    Queue.Add(action);
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
                        Func<Task> scoped = null;

                        //get from list
                        lock (Queue)
                        {
                            if (Queue.Count > 0)
                            {
                                scoped = Queue[0];
                            }
                        }

                        //execute and wait
                        if (scoped != null)
                        {
                            await scoped();

                            //update list
                            lock (Queue)
                                Queue.Remove(Queue[0]);
                        }

                        await Task.Delay(10000);
                    }
                }catch(Exception excpt)
                {
                    Console.WriteLine(excpt.Message) ;
                }
            } while (true) ;
        }

        public static async Task SpeakTranslate(string cmd)
        {
            string msg;
            Languages? src, trg;
            if (GetLanguages(cmd, out src, out trg, out msg))
            {
                var Target = trg.Value;

                msg = await Translate.TranslateCore(msg, false, Target);

                switch (Target)
                {
                    case Languages.ar:
                        await QueueAdd(async () => await SpeakArabic(msg));
                        break;
                    case Languages.de:
                        await QueueAdd(async () => await SpeakGerman(msg));
                        break;
                    case Languages.el:
                        await QueueAdd(async () => await SpeakGreece(msg));
                        break;
                    case Languages.en:
                        await QueueAdd(async () => await SpeakEnglish(msg));
                        break;
                    case Languages.es:
                        await QueueAdd(async () => await SpeakSpanish(msg));
                        break;
                    case Languages.fr:
                        await QueueAdd(async () => await SpeakFrench(msg));
                        break;
                    case Languages.it:
                        await QueueAdd(async () => await SpeakItalian(msg));
                        break;
                    case Languages.ja:
                        await QueueAdd(async () => await SpeakJapanese(msg));
                        break;
                    case Languages.pt:
                        await QueueAdd(async () => await SpeakPortuga(msg));
                        break;
                    case Languages.ru:
                        await QueueAdd(async () => await SpeakRussian(msg));
                        break;
                    case Languages.zh:
                        await QueueAdd(async () => await SpeakChinese(msg));
                        break;
                }


            }
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
