using System.Text;

namespace AndreasReitberger.Shared.Core.Extensions
{
    public static class StackExtensions
    {
        #region Extensions

        public static string Concat<T>(this Stack<T> source, string separator)
        {
            StringBuilder sb = new();
            for (int i = source.Count - 1; i >= 0; i--)
            {
                sb.Append(source.ElementAt(i));
                if (i > 0)
                {
                    sb.Append(separator);
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}
