#if __IOS__ || __MACCATALYST__
using Foundation;

namespace AndreasReitberger.Shared.Core.Platforms.iOS.Utilities
{
    public static class NSArrayConverter
    {
        public static NSArray ToNSArray(List<Dictionary<string, object>> dictionaryList)
        {
            return NSArray.FromObjects(dictionaryList.Select(
                dict => NSDictionaryConverter.ToNSDictionary(dict)).ToArray());
        }

        public static NSArray ToNSArray(Dictionary<string, object>[] dictionaryArray)
        {
            return ToNSArray(dictionaryArray.ToList());
        }
    }
}
#endif