using System.Text;

namespace AndreasReitberger.Shared.Core.Extensions
{
    public static class MathExtensions
    {
        #region Extensions

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        #endregion
    }
}
