using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities
{
    public static class LogUtility
    {
        public static void PrintLog(in string tag, in string message, Object context=null)
        {
            Debug.unityLogger.Log(tag,message, context);
        }        
        
        public static void PrintLogWarning(in string tag, in string message,Object context=null)
        {
            Debug.unityLogger.LogWarning(tag,message,context);
        }
        
        public static void PrintLogError(in string tag, in string message,Object context=null)
        {
            Debug.unityLogger.LogError(tag,message,context);
        }
        
        public static void PrintLogException(Exception exception,Object context=null)
        {
            Debug.unityLogger.LogException(exception, context);
        }
    }
}