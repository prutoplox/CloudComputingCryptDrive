namespace Cryptdrive
{
    class Logger
    {
        private Logger()
        {
            datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            logFilename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".log";

            // Log file header line
            string logHeader = logFilename + " is created.";
            if (!System.IO.File.Exists(logFilename))
            {
                WriteLine(System.DateTime.Now.ToString(datetimeFormat) + " " + logHeader, false);
            }
        }

        private readonly string datetimeFormat;
        private readonly string logFilename;
        public static Logger instance = new Logger();

        public void logDebug(string text)
        {
            writeFormattedLog(LogLevel.DEBUG, text);
        }

        public void logError(string text)
        {
            writeFormattedLog(LogLevel.ERROR, text);
        }

        public void logInfo(string text)
        {
            writeFormattedLog(LogLevel.INFO, text);
        }

        private void writeFormattedLog(LogLevel level, string text)
        {
            string pretext;
            switch (level)
            {
                case LogLevel.INFO:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [INFO]:    ";
                    break;

                case LogLevel.DEBUG:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [DEBUG]:   ";
                    break;

                case LogLevel.ERROR:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [ERROR]:   ";
                    break;

                default:
                    pretext = "";
                    break;
            }

            WriteLine(pretext + text);
        }

        private void WriteLine(string text, bool append = true)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(logFilename, append, System.Text.Encoding.UTF8))
                {
                    if (text != "")
                    {
                        writer.WriteLine(text);
                        GUI.instance.LogToTextBox(text);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        [System.Flags]
        private enum LogLevel
        {
            INFO,
            DEBUG,
            ERROR
        }
    }
}
