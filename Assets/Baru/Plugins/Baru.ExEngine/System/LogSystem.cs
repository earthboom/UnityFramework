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

    public static List<LogData> mLogData = new List<LogData>();
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

    public static void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {

    }

    public static string LogToString()
    {
        StringBuilder sb = new StringBuilder();

        return sb.ToString(); ;
    }
}
