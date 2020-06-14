using System.IO;
using System;
using System.Text;

namespace PasswordGenerator
{
    public sealed class Log : MainWindow
    {
        private static Log instance = null;
        private static readonly object padlock = new object();
        public string Date;

        public bool isStarted;

        Log()
        {
            isStarted = false;
        }

        public static Log Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Log();
                    }
                    return instance;
                }
            }
        }

        public void LogToFile(string message, bool start)
        {
            if(isStarted)
            {
                Date = DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss");
                StreamWriter sw = new StreamWriter("Logs.txt", true, Encoding.ASCII);
                if(start)
                    sw.Write(message + "\n");
                else
                    sw.Write($"[{Date}] {message}\n");
                sw.Close();
            }
        }

    }
}
