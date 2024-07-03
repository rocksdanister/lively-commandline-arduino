using System.Threading;

namespace Lively.IPC.Helpers
{
    public static class SingleInstanceUtil
    {
        public static bool IsAppMutexRunning(string mutexName)
        {
            Mutex mutex = null;
            try
            {
                return Mutex.TryOpenExisting(mutexName, out mutex);
            }
            finally
            {
                mutex?.Dispose();
            }
        }
    }
}
