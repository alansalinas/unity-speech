using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Reflection;

namespace UnitySpeechTester
{
    class Program
    {

        static ManualResetEvent _completed = null;

        static void Main(string[] args)
        {
            _completed = new ManualResetEvent(false);

            ActionBot speechBot = new ActionBot("../../Rules.xml", 1001);
            speechBot.StartServer();
            
            _completed.WaitOne(); // Suspend main thread
        }
        
    }
    
}
