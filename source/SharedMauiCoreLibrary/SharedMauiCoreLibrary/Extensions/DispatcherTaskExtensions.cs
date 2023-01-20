using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreasReitberger.Shared.Core.Extensions
{
    // Source: https://github.com/microsoft/Windows-task-snippets/blob/master/tasks/UI-thread-task-await-from-background-thread.md
    // Modifed: Yes
    // - Updated to work on .NET MAUI
    public static class DispatcherTaskExtensions
    {
        public static async Task<T> RunTaskAsync<T>(this Dispatcher dispatcher,
            Func<Task<T>> func, ThreadPriority priority = ThreadPriority.Normal)
        {
            TaskCompletionSource<T> taskCompletionSource = new();
            await dispatcher.RunTaskAsync(async () =>
            {
                try
                {
                    taskCompletionSource.SetResult(await func());
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            }, priority);
            return await taskCompletionSource.Task;
        }

        // There is no TaskCompletionSource<void> so we use a bool that we throw away.
        public static async Task RunTaskAsync(this Dispatcher dispatcher,
            Func<Task> func, ThreadPriority priority = ThreadPriority.Normal) =>
            await RunTaskAsync(dispatcher, async () => { await func(); return false; }, priority);

        public static async Task<T> DispatchAsync<T>(this IDispatcher dispatcher,
            Func<Task<T>> func, ThreadPriority priority = ThreadPriority.Normal)
        {
            TaskCompletionSource<T> taskCompletionSource = new();
            await dispatcher.DispatchAsync(async () =>
            {
                try
                {
                    taskCompletionSource.SetResult(await func());
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            }, priority);
            return await taskCompletionSource.Task;
        }

        // There is no TaskCompletionSource<void> so we use a bool that we throw away.
        public static async Task DispatchAsync(this IDispatcher dispatcher,
            Func<Task> func, ThreadPriority priority = ThreadPriority.Normal) =>
            await DispatchAsync(dispatcher, async () => { await func(); return false; }, priority);
    }
}
