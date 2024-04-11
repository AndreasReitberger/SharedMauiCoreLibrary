using System.Diagnostics;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public static class DebugLogger
    {
        /// <summary>
        /// A simple log method to write information to the debug console with a timestamp.
        /// </summary>
        /// <param name="message">The message which should be logged</param>
        /// <param name="releaseAction">If not DEBUG, instead of writing to the console, the passed action will be invoked.</param>
        public static void Log(string message, Action? releaseAction = null)
        {
#if DEBUG
            Debug.WriteLine($"{DateTime.Now}: {message}");
#else        
            releaseAction?.Invoke();
#endif
        }

        public static Exception? GetLastInnerException(Exception? exception)
        {
            Stack<Exception?> exceptions = new();
            exceptions.Push(exception);
            while (exceptions.Count > 0)
            {
                Exception? innerException = exceptions.Pop()?.InnerException;
                if (innerException != null)
                {
                    exceptions.Push(innerException);
                }
                else return innerException;
            }
            return exception;
        }
    }
}
