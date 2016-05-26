using SpeechModule;

namespace UnitySpeechTester
{
    class ActionBot : SpeechListener
    {
        public ActionBot(string filename) : base(filename) { }
        public ActionBot(int port) : base(port) { }
        public ActionBot(string filename, int port) : base(filename, port) { }
        public ActionBot(int port, string address) : base(port, address) { }
        public ActionBot(string filename, int port, string address) : base(filename, port, address) { }

        public void MoveForward()
        {
            System.Diagnostics.Debug.WriteLine("MoveForward function called");
        }

        public void MoveBack()
        {
            System.Diagnostics.Debug.WriteLine("MoveBack function called");
        }

        public void MoveLeft()
        {
            System.Diagnostics.Debug.WriteLine("MoveLeft function called");
        }

        public void MoveRight()
        {
            System.Diagnostics.Debug.WriteLine("MoveRight function called");
        }
    }
}
