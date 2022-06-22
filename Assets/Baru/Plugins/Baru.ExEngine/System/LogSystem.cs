#define UNITY_LOG_USE

using System.Collections.Generic;
using System.IO;
using System.Text;

using UniRx;
using UniRx.Diagnostics;

using UnityEngine;

public class LogSystem : MonoBehaviour
{
#if UNITY_LOG_USE
    public const string ENABLE_LOGS = "ENABLE_LOG";
#else
    public const string ENABLE_LOGS = "UNABLE_LOG";
#endif

    static readonly string gFileName = "GameLog.log";

    public class LogData
    {
        public LogData(LogType type, string title, string message)
        {
            mType = type;
            mTitle = title;
            mMessage = message;
        }

        public LogType mType;
        public string mTitle;
        public string mMessage;
    }

    public static List<LogData> mLogDatas = new List<LogData>();
    public static bool mUse;
    public static bool mUnity;
    public static bool mFile;
    public static string mFullPath = Path.Combine(Application.temporaryCachePath, System.DateTime.Now.ToString("yyyyMMddhhmmss"));

    public static System.Action[] mLogActions = new System.Action[(int)LogType.Exception + 1];

    public static string mScreenshotErrorFileName = System.DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + "ErrorScreenShot.png";
    public static string mScreenshotWarningFileName = System.DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + "WarningScreenShot.png";

    public static string LastLogMessage { get; set; }

    public static bool IsDebugBuild
    {
        get
        {
            return Debug.isDebugBuild;
        }
    }

    public static string GetLogFileName(string fileName = null)
    {
        if (string.IsNullOrEmpty(fileName))
            return $"{mFullPath}_{gFileName}";
        else
            return $"{mFullPath}_{fileName}";
    }

    public static void Init()
    {
#if UNITY_EDITOR
        mUse = true;
        mUnity = true;
        mFile = true;
#else
        mUse = false;
        mUnity = false;
        mFile = false;
#endif

        Application.logMessageReceived += Application_logMessageReceived;
    }

    public static void Destroy()
    {
        if (mFile)
            File.WriteAllText(GetLogFileName(), LogToString());
    }

    static void InitUniRx()
    {
#if UNITY_EDITOR
        ObservableLogger.Listener.LogToUnityDebug();
        ObservableLogger.Listener.Subscribe(x =>
        {
            switch (x.LogType) 
            {
                case LogType.Error:
                    break;
                case LogType.Assert:
                    break;
                case LogType.Warning:
                    break;
                case LogType.Log:
                    break;
                case LogType.Exception:
                    break;
            }
            Log("ObservableLogger.Listener.Subscribe", x.ToString());
        });
#else
        ObservableLogger.Listener.LogToUnityDebug();
        ObservableLogger.Listener.Subscribe(x =>
        {
            switch (x.LogType) 
            {
                case LogType.Error:
                    break;
                case LogType.Assert:
                    break;
                case LogType.Warning:
                    break;
                case LogType.Log:
                    break;
                case LogType.Exception:
                    break;
            }
        });
#endif
    }

    public static void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        LastLogMessage = $"{type}:{condition} - {stackTrace}";

        if (mFile)
            mLogDatas.Add(new LogData(type, condition, stackTrace));

        switch (type) 
        {
            case LogType.Error:
                if (mFile)
                    System.IO.File.WriteAllText(GetLogFileName(), LogToString());
                break;
            case LogType.Assert:
                break;
            case LogType.Warning:
                break;
            case LogType.Log:
                break;
            case LogType.Exception:
                if (mFile)
                    System.IO.File.WriteAllText(GetLogFileName(), LogToString());
                break;
        }
    }

    public static void Turn(bool on)
    {
        if (on)
        {
            mUse = true;
            mUnity = true;
        }
        else
        {
            mUse = false;
            mUnity = false;
        }
    }

    public static string LogToString()
    {
        StringBuilder sb = new StringBuilder();
        for(int i=0; i< mLogDatas.Count; ++i)
        {
            sb.AppendLine($"{mLogDatas[i].mType} : ");
            if (string.IsNullOrEmpty(mLogDatas[i].mMessage))
            {
                sb.AppendLine(mLogDatas[i].mTitle);
            }
            else
            {
                sb.AppendFormat("{0} - {1}", mLogDatas[i].mTitle, mLogDatas[i].mMessage);
                sb.AppendLine();
            }
        }

        return sb.ToString(); ;
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Log(string title, string message, params object[] param)
    {
        Log(title, string.Format(message, param));
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Log(string title, string message)
    {
        if (false == mUse)
            return;

        if (mUnity)
        {
            if (string.IsNullOrEmpty(message))
            {
                Debug.Log(title);
            }
            else
            {
                Debug.LogFormat("{0} : {1}", title, message);
            }
        }
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Warning(string title, string message, params object[] param)
    {
        Warning(title, string.Format(message, param));
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Warning(string title, string message)
    {
        if (false == mUse)
            return;

        if (mUnity)
        {
            if (string.IsNullOrEmpty(message))
            {
                Debug.LogWarning(title);
            }
            else
            {
                Debug.LogWarningFormat("{0} : {1}", title, message);
            }
        }
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Error(string title, string message, params object[] param)
    {
        Error(title, string.Format(message, param));
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Error(string title, string message)
    {
        if (false == mUse)
            return;

        if (mUnity)
        {
            if (string.IsNullOrEmpty(message))
            {
                Debug.LogError(title);
            }
            else
            {
                Debug.LogErrorFormat("{0} : {1}", title, message);
            }
        }
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Exception(string title, System.Exception exception, params object[] param)
    {
        Exception(title, exception);
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void Exception(string title, System.Exception exception, UnityEngine.Object context = null)
    {
        if (false == mUse)
            return;

        if (mUnity)
        {
            Debug.LogException(exception, context);
        }
    }

    [System.Diagnostics.Conditional(ENABLE_LOGS)]
    public static void MessageClassFuncName(string message = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Diagnostics.StackFrame stackFramePre = stackTrace.GetFrame(1);

        string title = string.Format($"{stackFramePre.GetMethod().ReflectedType.Name}::{stackFramePre.GetMethod().Name}");
        Log(title, message);
    }

    public static string GetPreStackInfo(int framePos = 2)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Diagnostics.StackFrame stackFramePre = stackTrace.GetFrame(1);

        string info = string.Format("[{3}::{4}-{0}]",
            stackFramePre.GetFileName(),
            stackFramePre.GetFileLineNumber(),
            stackFramePre.GetFileColumnNumber(),
            stackFramePre.GetMethod().ReflectedType.Name,
            stackFramePre.GetMethod().Name);

        return info;
    }
}
