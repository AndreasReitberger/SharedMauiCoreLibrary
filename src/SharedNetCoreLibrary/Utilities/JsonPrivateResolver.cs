#if NEWTONSOFT
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonProperty = Newtonsoft.Json.Serialization.JsonProperty;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public class JsonPrivateResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (!prop.Writable)
            {
                PropertyInfo? property = member as PropertyInfo;
                bool hasPrivateSetter = property?.GetSetMethod(true) != null;
                prop.Writable = hasPrivateSetter;
            }
            return prop;
        }
    }
}
#endif