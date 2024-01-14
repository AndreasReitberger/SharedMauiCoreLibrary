namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class TypeConvertHelper
    {
        #region Converts
        public static T ConvertObject<T>(object input) => (T)Convert.ChangeType(input, typeof(T));
        
        #endregion
    }
}
