using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Assets.Scripts.Utility
{
    public enum ELogCategory
    {
        General,
        Control,
        Navigation,
        Animation,
        Combat
    };

    public enum ELogLevel
    {
        System = 0,
        Assert,
        Error,
        Warning,
        Trace
    };

    public class Logger : MonoBehaviour
    {
        private class CategoryConfig
        {
            public ELogLevel _level;
            public String _logFile;
            public bool _useIndependantLog;

            public CategoryConfig(ELogLevel level, String file, bool useIndependant)
            {
                _level = level;
                _logFile = file;
                _useIndependantLog = useIndependant;
            }
        }

        private Dictionary<ELogCategory, CategoryConfig> configuration = new Dictionary<ELogCategory, CategoryConfig>()
        {
            {ELogCategory.Control,      new CategoryConfig(ELogLevel.Trace, "Control.log",      false)},
            {ELogCategory.Navigation,   new CategoryConfig(ELogLevel.Trace, "NavLog.log",       true)},
            {ELogCategory.Animation,    new CategoryConfig(ELogLevel.Trace, "AnimationLog.log", true)},
            {ELogCategory.Combat,       new CategoryConfig(ELogLevel.Trace, "CombatLog.log",    true)}
        };

        static private String generalLog = "ApplicationLog.log";

        public bool EchoToConsole = true;
        public bool AddTimeStamp = true;

        public bool BreakOnError = true;
        public bool BreakOnAssert = true;

        private Dictionary<String, StreamWriter> logWriters = new Dictionary<string, StreamWriter>();

        private static Logger _Singleton = null;

        public static Logger Singleton
        {
            get { return _Singleton; }
        }

        public void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (null != _Singleton)
            {
                UnityEngine.Debug.LogError("Logger.cs: Multiple Logger Singletons Exist!");
                return;
            }
            _Singleton = this;

            // Initialize the General Log file
            logWriters.Add(generalLog, new StreamWriter(generalLog, true, System.Text.Encoding.UTF8));
        }

        private void Write(ELogCategory category, ELogLevel level, String message)
        {
            // Ensure the level is within desired levels
            CategoryConfig conf = configuration[category];
            if (level > conf._level)
            {
                return;
            }

            // Get the desired Output Stream for the Category
            StreamWriter outputStream;
            if (conf._useIndependantLog)
            {
                if (logWriters.ContainsKey(conf._logFile))
                {
                    outputStream = logWriters[conf._logFile];
                }
                else
                {
                    outputStream = new StreamWriter(conf._logFile, true, System.Text.Encoding.UTF8);
                    logWriters.Add(conf._logFile, outputStream);
                }
            }
            else
            {
                // Default File Writer
                outputStream = logWriters[generalLog];
            }

            // Add timestamp to the message
            if (AddTimeStamp)
            {
                DateTime current = DateTime.Now;
                message = string.Format("[{0:HH:mm:ss:fff}] {1:7} : {2:10} - {3}",
                                        current, level.ToString("G"), 
                                        category.ToString("G"), 
                                        message);
            }

            // Commit the message to the file
            if (null != outputStream)
            {
                outputStream.WriteLine(message);
                outputStream.Flush();
            }

#if !FINAL
            // Echo to the Console if a debug
            if (EchoToConsole)
            {
                if (ELogLevel.Trace == level)
                {
                    UnityEngine.Debug.Log(message);
                }
                else if (ELogLevel.Warning == level)
                {
                    UnityEngine.Debug.LogWarning(message);
                }
                else // Both Error and Assert
                {
                    UnityEngine.Debug.LogError(message);
                }
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        public static void LogMessage(ELogCategory category, ELogLevel level, String message)
        {
#if !FINAL
            if (null != Logger.Singleton)
            {
                Logger.Singleton.Write(category, level, message);
            }
            else if (ELogLevel.System == level)
            {
                // Fallback if the debugging system hasn't been initialized yet.
                UnityEngine.Debug.Log(message);
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        public static void Assert(ELogCategory category, bool condition, String message)
        {
#if !FINAL
            if (condition)
            {
                return;
            }

            if (null != Logger.Singleton)
            {
                Logger.Singleton.Write(category, ELogLevel.Assert, message);

                if (Logger.Singleton.BreakOnAssert)
                {
                    UnityEngine.Debug.Break();
                }
            }
#endif
        }

        public void Cleanup()
        {
            foreach (var item in logWriters.Values)
            {
                item.Close();
            }
            logWriters.Clear();
        }

        public void OnDestroy()
        {
            Cleanup();
        }
    }
}
