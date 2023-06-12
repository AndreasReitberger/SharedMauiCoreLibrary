using System.Reflection;
using System.Xml;

namespace AndreasReitberger.Shared.Core.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<List<T>> Split<T>(this List<T> source, int size)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
