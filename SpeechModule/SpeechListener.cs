using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Reflection;

namespace SpeechModule
{
    public class SpeechListener
    {

        private const string LOCAL_IP_ADDRESS = "127.0.0.1";
        private const int PORT = 11501;
        private const string RULE_DICTIONARY_FILENAME = "Rules.xml";

        private Hashtable ruleDictionary = new Hashtable();

        Thread receiveThread;
        UdpClient client;
        IPAddress ipAddress;
        IPEndPoint ipEndpoint;
        string incomingString = "";

        public SpeechListener()
        {
            client = new UdpClient(PORT);
            ipAddress = IPAddress.Parse(LOCAL_IP_ADDRESS);
            ipEndpoint = new IPEndPoint(ipAddress, PORT);
            LoadRules(RULE_DICTIONARY_FILENAME);
        }

        public SpeechListener(string filename)
        {
            client = new UdpClient(PORT);
            ipAddress = IPAddress.Parse(LOCAL_IP_ADDRESS);
            ipEndpoint = new IPEndPoint(ipAddress, PORT);
            LoadRules(filename);
        }

        public SpeechListener(int port)
        {
            client = new UdpClient(port);
            ipAddress = IPAddress.Parse(LOCAL_IP_ADDRESS);
            ipEndpoint = new IPEndPoint(ipAddress, port);
            LoadRules(RULE_DICTIONARY_FILENAME);
        }

        public SpeechListener(string filename, int port)
        {
            client = new UdpClient(port);
            ipAddress = IPAddress.Parse(LOCAL_IP_ADDRESS);
            ipEndpoint = new IPEndPoint(ipAddress, port);
            LoadRules(filename);
        }

        public SpeechListener(int port, string address)
        {
            client = new UdpClient(port);
            ipAddress = IPAddress.Parse(address);
            ipEndpoint = new IPEndPoint(ipAddress, port);
            LoadRules(RULE_DICTIONARY_FILENAME);
        }

        public SpeechListener(string filename, int port, string address)
        {
            client = new UdpClient(port);
            ipAddress = IPAddress.Parse(address);
            ipEndpoint = new IPEndPoint(ipAddress, port);
            LoadRules(filename);
        }

        public void StartServer()
        {
            receiveThread = new Thread(new ThreadStart(Listen));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }


        private void Listen()
        {
            while (true)
            {
                try
                {
                    byte[] data = client.Receive(ref ipEndpoint);
                    incomingString = Encoding.UTF8.GetString(data);
                    ProcessCommand(incomingString);
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.WriteLine(err.ToString());
                }
            }
        }

        private void LoadRules(string filename)
        {
            try {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlNodeList ruleNodes = doc.DocumentElement.SelectNodes("/RuleDictionary/Rule");

                foreach (XmlNode ruleNode in ruleNodes)
                {
                    string ruleId = ruleNode.Attributes["id"].Value;
                    string spokenInput = ruleNode["SpokenInput"].InnerText;
                    string actionMethod = ruleNode["ActionMethod"].InnerText;

                    ruleDictionary.Add(spokenInput, actionMethod);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private void ProcessCommand(string inputString)
        {
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(ruleDictionary[inputString].ToString());
            theMethod.Invoke(this, null);
        }


        }   //  End of class SpeechListener
    }
