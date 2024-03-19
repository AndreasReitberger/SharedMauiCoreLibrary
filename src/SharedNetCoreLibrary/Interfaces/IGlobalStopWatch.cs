namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IGlobalStopWatch
    {
        long StopWatchAction(Action action);
        Task<long> StopWatchActionAsync(Func<Task> function);
    }
}
