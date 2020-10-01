using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebMatBot
{
    public static class Commands
    {
        public static string ProjectLink { get; set; }
        public static string TetrisLink { get; set; }
        public static bool Speaker { get; set; } = false;

        public static IDictionary<string, Action<string>> List = new Dictionary<string, Action<string>>()
        {
            {"!Projeto" , async (string text) => await Projeto() },
            {"!Duelo" , async (string text) => await Duelo() },
            {"!Tetris" , async (string text) => await Tetris() },
            {"!Pesquisa" , async (string text) => await Pesquisa() },
            {"!Salve" , async (string text) => await Salve() },
            {"!Caiu" , async (string text) => await Caiu() },
            {"!GitHub" , async (string text) => await GitHub() },
            {"!Bot" , async (string text) => await GitHub() },
            {"!Exclamação" , async (string text) =>  await Exclamacao()},
            {"!DeiF5" ,async (string text) => await Cache.Respond() },
            {"!Top", async(string text) => await Counters.Respond("!top","Seu vício de falar top já está acumulado em {n} vezes...") },
            {"!Speak", (string text) => Speak(text) } //to activate spekaer... goes to console and type "!setspeaker true"
        };

        private static async Task Projeto()
        {
            await Core.Respond(ProjectLink);
        }

        private static async Task GitHub()
        {
            await Core.Respond("https://github.com/WebMat1/WebMatTwitchBot");
        }

        private static async Task Tetris()
        {
            await Core.Respond(TetrisLink);
        }

        private static async Task Salve()
        {
            await Core.Respond("Salve alá Igão... FortBush CurseLit");
        }

        private static async Task Caiu()
        {
            await Core.Respond("Keep Calm... just clik it: www.twitch.tv/webmat1" );
        }

        private static async Task Exclamacao()
        {
            foreach(var item in List)
                await Core.Respond(item.Key);
        }

        private static async Task Duelo()
        {
            await Core.Respond("Tá marcado... Dia 02/10/2020... às 16h... o nosso duelo de TETRIS entre FredFp e WebMat... Fisica vs Programação... O perdedor dará um sub de presente pro ganhador... Torce pra mim hein... birlll!!! ");
            await Core.Respond("So... its Scheduled... 02/10/2020... at 4 PM... The TETRIS duel between WebMat and FredFp... Its mean Physics vs Programming... The loser will give a  sub gift to winner... so... pls cheering me... ");
        }

        private static async Task Pesquisa()
        {
            await Core.Respond("https://forms.gle/nzF1M8DaH1c38Pce6");
        }

        private static async void Speak(string textToSpeech, bool wait = false)
        {
            if (!Speaker)
            {
                await Core.Respond("O Speaker está off... peça o streamer para aciona-lo...");
                return;
            }

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

    }
}
