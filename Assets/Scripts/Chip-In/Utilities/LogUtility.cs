using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities
{
    public static class LogUtility
    {
        private const bool UseLog = false;

        public static void PrintLog(in string tag, in string message, Object context = null)
        {
#if UseLOG
            Debug.unityLogger.Log(tag, message, context);
#endif
        }

        public static void PrintLogWarning(in string tag, in string message, Object context = null)
        {
#if UseLOG
            Debug.unityLogger.LogWarning(tag, message, context);
#endif
        }

        public static void PrintLogError(in string tag, in string message, Object context = null)
        {
#if UseLOG
            Debug.unityLogger.LogError(tag, message, context);
#endif
        }

        public static void PrintLogException(Exception exception, Object context = null)
        {
#if UseLOG
            Debug.unityLogger.LogException(exception, context);
#endif
        }

        public static void PrintDefaultOperationCancellationLog(in string tag)
        {
            PrintLog(tag,"Ongoing operation was cancelled");
        }
    }
}