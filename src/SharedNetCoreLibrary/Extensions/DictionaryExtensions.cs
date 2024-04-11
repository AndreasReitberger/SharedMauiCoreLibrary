namespace AndreasReitberger.Shared.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<T> ToList<T>(this Dictionary<T, List<T>> source) where T : notnull
        {
            List<T> list = [];
            if (source == null) return list;
            foreach (KeyValuePair<T, List<T>> pairs in source)
            {
                list.AddRange(pairs.Value);
            }
            return list;
        }
    }
}
