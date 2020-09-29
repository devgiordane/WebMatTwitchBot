using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebMatBot
{
    public static class Cache
    {
        public static IList<string> Messages = new List<string>();

        public static void AddToCacheMessage(string input)
        {
            //:webmat1!webmat1@webmat1.tmi.twitch.tv PRIVMSG #{user} :Teste

            //get the owner of message e save this
            var arrayInput = input.Split("PRIVMSG");

            if (arrayInput.Length >= 2)
            {
                var msg = arrayInput[0].Split("!")[0] + " "+arrayInput[1].Split("#webmat1")[1];
                Messages.Add(msg + ";");

                if (Messages.Count > 10) Messages.RemoveAt(0);
            }

        }

        public static async Task Respond()
        {
            foreach(var item in Messages)
                await Core.Respond(item); // maximo de 500 caracteres por mensagem na twitch

        }
    }
}
