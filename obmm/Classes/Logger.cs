using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace OblivionModManager
{
    public class Logger
    {
        Logger.LogLevel m_debug = Logger.LogLevel.Low;
        string _eventLogSource = "tmm";
        string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string logFileName = "tmm.Log";
        public string logFile { get; private set; }

        public Logger()
        {
            logFile = Path.Combine(logFilePath, logFileName); ;
        }

        public Logger(LogLevel level)
        {
            _Debug = level;
        }
        public Logger(string eventSource)
        {
            EventLogSource = eventSource;
        }
        public Logger.LogLevel _Debug
        {
            get { return m_debug; }
            set { m_debug = value; }

        }

        public string EventLogSource
        {
            get { return _eventLogSource; }
            set
            {
                _eventLogSource = value;
                try
                {
                    if (!System.Diagnostics.EventLog.Exists("tmm") || !System.Diagnostics.EventLog.SourceExists(_eventLogSource))
                    {
                        System.Diagnostics.EventLog.CreateEventSource(_eventLogSource, "tmm");
                    }
                }
                catch (System.Security.SecurityException se)
                {
                    // no rights?
                    Console.WriteLine("Could not check or create event log: " + se.Message);
                }
            }
        }

        public enum LogLevel
        {
            None = 0,
            Low = 1,
            Medium = 2,
            High = 3,
            Error = 4,
            Warning = 5
        }

        // unused for now
        public Logger.LogLevel setLogLevel(string level)
        {
            LogLevel loglevel = LogLevel.None;
            switch (level.ToLower())
            {
                case "none":
                    loglevel = LogLevel.None;
                    break;
                case "low":
                    loglevel = LogLevel.Low;
                    break;
                case "medium":
                    loglevel = LogLevel.Medium;
                    break;
                case "high":
                    loglevel = LogLevel.High;
                    break;
                default:
                    loglevel = LogLevel.Low;
                    break;
            }

            _Debug = loglevel;
            return loglevel;
        }

        public void WriteToLog(string message, LogLevel level)
        {
            // enforce home directory
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

            //Initialize our write flag to false
            bool logMessage = false;
            bool logToFile = false;

            //Default the Event log type to information
            EventLogEntryType logType = EventLogEntryType.Information;

            //Do we need to log this message?
            switch (level)
            {
                case Logger.LogLevel.Error:
                    //Always write errors and warnings!!
                    logMessage = true;
                    logToFile = true;

                    //Set the Event log type to Error
                    logType = EventLogEntryType.Error;

                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case Logger.LogLevel.Warning:
                    //Always write warnings!!
                    logMessage = true;
                    logToFile = true;

                    //Set the Event log type to Warning
                    logType = EventLogEntryType.Warning;

                    MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case Logger.LogLevel.High:
                    //Always write to file only with High value!!
                    logMessage = false;
                    logToFile = (level <= m_debug) ? true : false;

                    break;
                default:
                    //If the log level is less than the Debug property
                    // write to the log
                    logToFile = (level <= m_debug) ? true : false;
                    break;
            }

            if (logMessage || logToFile)
            {
                try
                {
                    if (logToFile)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(String.Format("{0}:{1,-7}: {2}", DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss"), level, message));
                        using (StreamWriter swLogFile = File.AppendText(logFile))
                        {
                            swLogFile.Write(sb.ToString());
                        }
                    }
                    if (logMessage)
                    {
                        //                        if (!System.Diagnostics.EventLog.Exists("Oce") || !System.Diagnostics.EventLog.SourceExists(_eventLogSource))
                        //                        {
                        //                            System.Diagnostics.EventLog.CreateEventSource(_eventLogSource, "Oce");
                        //                        }
                        EventLog myLog = new EventLog("tmm");
                        myLog.Source = _eventLogSource;
                        myLog.WriteEntry(message, logType);
                        myLog.Close();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Could not write event log: " + exception.Message);
                    //                  several instances may be accessing the log file... 
                    //                  so we ignore this exception.
                    //                    throw new ApplicationException("Log writing failed.", exception);
                }

            }
        }

    }
}
