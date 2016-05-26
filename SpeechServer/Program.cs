using System.Threading;

namespace SpeechServer
{
    class Program
    {
        static ManualResetEvent _completed = null;

        public static int SpeechBotCallback(string t)
        {
            UDPService udp = new UDPService();
            udp.Send(t);
            return 0;
        }

        static void Main(string[] args)
        {
            _completed = new ManualResetEvent(false);
            SpeechBot sb = new SpeechBot();
            
            sb.StartRecognition(SpeechBotCallback);
            _completed.WaitOne(); // Suspend main thread
            sb.Dispose();

        }
    }
}
