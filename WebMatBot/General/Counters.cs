using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMatBot
{
    public class Counters
    {
        private static IDictionary<string, int> List = new Dictionary<string, int>()
        {
            {"!top", 0 },
        };

        public static void CheckCounter(string input)
        {
            //verifica counters
            lock (List)
            {
                int total = List.Count;
                for (int i = 0; i < total; i++)
                    if (input.ToLower().Contains(List.ElementAt(i).Key.ToLower())) List[List.ElementAt(i).Key]++;
            }
        }

        public static async Task Respond(string key, string msg)
        {
            string qtd;
            lock (List)
            {
                qtd = List[key].ToString("00");
            }
            await Engine.Respond(msg.Replace("{n}", qtd));
        }
    }
}
