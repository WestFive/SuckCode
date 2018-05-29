using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test1
{
    public class P2p
    {
        public string receiver { get; set; }
        public string msg { get; set; }


        public P2p(string receiver,string msg)
        {
            this.receiver = receiver;
            this.msg = msg;
        }

    }
}
