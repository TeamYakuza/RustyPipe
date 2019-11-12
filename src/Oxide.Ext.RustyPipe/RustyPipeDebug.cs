using System;
using Oxide.Core;

namespace Oxide.Ext.RustyPipe
{
    public class RustyPipeDebug
    {
        public static void LogError(string message,params object[] args)
        {
            Interface.Oxide.LogError($"[RustyPipe]: {message}", args);
        }
        public static void Log(string message, params object[] args)
        {
            Interface.Oxide.LogInfo($"[RustyPipe]: {message}", args);
            
        }
       
        public static void LogWarning(string message, params object[] args)
        {
            Interface.Oxide.LogWarning($"[RustyPipe]: {message}", args);
        }
    }
}
