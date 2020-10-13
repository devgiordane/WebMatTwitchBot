using System;
using System.Collections.Generic;
using System.Text;

namespace WebMatBot
{
    public class Command
    {

        public Command(string _Key, Action<string, string> _Action, string _Description) {
            Key = _Key;
            Action = _Action;
            Description = _Description;
        }
        public string Key { get; set; }
        public Action<string, string> Action { get; set; }

        public string Description { get; set; }
    }
}
