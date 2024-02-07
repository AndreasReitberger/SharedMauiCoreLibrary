namespace AndreasReitberger.Shared.Core.Utilities
{
    public static class CollectionHelper
    {
        public static IEnumerable<List<T>> Split<T>(List<T> source, int size)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
