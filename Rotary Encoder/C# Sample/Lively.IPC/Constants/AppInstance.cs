using System;

namespace Lively.IPC.Constants
{
    internal static class AppInstance
    {
        public static string UniqueAppName { get; } = "LIVELY:DESKTOPWALLPAPERSYSTEM";
        public static string PipeServerName { get; } = UniqueAppName + Environment.UserName;
        public static string GrpcPipeServerName { get; } = "Grpc_" + PipeServerName;
    }
}
