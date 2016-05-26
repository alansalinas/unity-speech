using System;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Recognition;

namespace SpeechServer
{
    public class SpeechBot
    {

        private const string grammarFileName = "DefaultGrammar.xml";
        private const string grammarRootRule = "rootLevel";

        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        public delegate int CallbackMethod(string result);
        CallbackMethod callbackMethod;

        public void StartRecognition(CallbackMethod method)
        {
            callbackMethod = method;
            LoadGrammarFile();
            recognizer.SpeechRecognized += recognizer_SpeechRecognized; // Set the event to be called on recognition success
            recognizer.SetInputToDefaultAudioDevice(); // Set the input of the speech recognizer to the default audio device
            recognizer.RecognizeAsync(RecognizeMode.Multiple); // Start continuous recognition in an asynchronic task
            
        }
        
        private void LoadGrammarFile()
        {
            try
            {
                Grammar gr = new Grammar(grammarFileName, grammarRootRule);
                recognizer.LoadGrammar(gr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Could not load grammar file " + grammarFileName);
            }
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.Result.Text);
            callbackMethod(e.Result.Text);
        }

        public void Dispose()
        {
            recognizer.Dispose(); // Dispose the speech recognition engine
        }

    }   // End of class SpeechBot
}
