
using System.Text.Json.Serialization.Metadata;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonOptionsProvider
    {
        #region Converts

        public static JsonSerializerOptions CombinedOptions(params IJsonTypeInfoResolver[] resolvers)
        {
            return new JsonSerializerOptions
            {
                TypeInfoResolver = CombineResolvers(resolvers)
            };
        }

        public static IJsonTypeInfoResolver? CombineResolvers(params IJsonTypeInfoResolver[] resolvers)
        {
            try
            {
                return JsonTypeInfoResolver.Combine(resolvers);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}