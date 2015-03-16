using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Assets.Scripts.Utility
{
    public class Logger : MonoBehaviour
    {
        private enum EMessageType
        {
            Trace,
            Warning,
            Error,
            Assert
        };

        public string LogFile = "Log.txt";
        public bool EchoToConsole = true;
        public bool AddTimeStamp = true;

        public bool BreakOnError = true;
        public bool BreakOnAssert = true;

        private StreamWriter _OutputStream;

        private static Logger _Singleton = null;

        public static Logger Singleton
        {
            get { return _Singleton; }
        }

        public void Awake()
        {
            if (null != _Singleton)
            {
                UnityEngine.Debug.LogError("Logger.cs: Multiple Logger Singletons Exist!");
                return;
            }
            _Singleton = this;

            _OutputStream = new StreamWriter(LogFile, true, System.Text.Encoding.UTF8);
        }

        private void Write(EMessageType type, string message)
        {

            if (AddTimeStamp)
            {
                DateTime current = DateTime.Now;
                message = string.Format("[{0:HH:mm:ss:fff}] {1:7} : {2}",
                                        current, type.ToString("G"), message);
            }

            if (null != _OutputStream)
            {
                _OutputStream.WriteLine(message);
                _OutputStream.Flush();
            }
#if !FINAL
            if (EchoToConsole)
            {
                if (EMessageType.Trace == type)
                {
                    UnityEngine.Debug.Log(message);
                }
                else if (EMessageType.Warning == type)
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
        public static void Trace(string Message)
        {
#if !FINAL
            if (null != Logger.Singleton)
            {
                Logger.Singleton.Write(EMessageType.Trace, Message);
            }
            else
            {
                // Fallback if the debugging system hasn't been initialized yet.
                UnityEngine.Debug.Log(Message);
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        public static void Warning(string Message)
        {
#if !FINAL
            if (null != Logger.Singleton)
            {
                Logger.Singleton.Write(EMessageType.Warning, Message);
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        public static void Error(string Message)
        {
#if !FINAL
            if (null != Logger.Singleton)
            {
                Logger.Singleton.Write(EMessageType.Error, Message);

                if (Logger.Singleton.BreakOnError)
                {
                    UnityEngine.Debug.Break();
                }
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        public static void Assert(bool condition, string Message)
        {
#if !FINAL
            if (condition)
            {
                return;
            }

            if (null != Logger.Singleton)
            {
                Logger.Singleton.Write(EMessageType.Assert, Message);

                if (Logger.Singleton.BreakOnAssert)
                {
                    UnityEngine.Debug.Break();
                }
            }
#endif
        }

        public void OnDestroy()
        {
            if (null != _OutputStream)
            {
                _OutputStream.Close();
                _OutputStream = null;
            }
        }
    }
}
