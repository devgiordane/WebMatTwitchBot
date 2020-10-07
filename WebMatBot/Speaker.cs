using System;
using System.Collections.Generic;
using System.Text;

namespace WebMatBot
{
    public class Speaker : ISpeaker
    {
        public string Voice { get; set; }

        public string Alert { get; set; }

        public string Diction { get; set; }
    }

    public interface ISpeaker
    {
        public string Voice { get; set; }

        public string Alert { get; set; }

        public string Diction { get; set; }

    }
}
