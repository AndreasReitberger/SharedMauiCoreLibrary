#if __IOS__ || __MACCATALYST__
using Foundation;

namespace AndreasReitberger.Shared.Core.Platforms.MacCatalyst.Utilities
{
    public static class NSDictionaryConverter
    {
        public static Dictionary<string, object> ToDictionaryFromNSObject(NSDictionary<NSString, NSObject> dictionary)
        {
            Dictionary<string, object> dict = new();
            if (dictionary.Values.Length > 0)
            {
                NSObject first = dictionary.Values[0];
                if (first is NSArray arrary || first is NSDictionary tempDict)
                {
                    throw new Exception($"Value type of passed NSDictionary is {first.GetType()}." +
                        $" Use for this type '{(first.GetType() == typeof(NSArray) ? "ToDictionaryFromNSArray" : "ToDictionaryFromNSDictionary")}'!");
                }
            }
            string[] keys = dictionary.Keys.Select(k => k.ToString()).ToArray();
            NSObject[] values = dictionary.Values.Select(v => v).ToArray();

            dict = keys.Zip(values, (k, v) => new { Key = k, Value = v })
                                    .ToDictionary(x => x.Key, x => x.Value as object);
            return dict;
        }

        public static Dictionary<string, Dictionary<string, object>> ToDictionaryFromNSDictionary(NSDictionary<NSString, NSObject> dictionary)
        {
            Dictionary<string, Dictionary<string, object>> dict = new();
            if (dictionary.Values.Length > 0)
            {
                NSObject first = dictionary.Values[0];
                if (first is NSArray arrary)
                {
                    throw new Exception($"Value type of passed NSDictionary is {first.GetType()}." +
                        $" Use for this type '{"ToDictionaryFromNSArray"}'!");
                }
            }
            string[] keys = dictionary.Keys.Select(k => k.ToString()).ToArray();
            Dictionary<string, object>[] values = dictionary.Values.Select(v => ToDictionaryFromNSObject(v as NSDictionary<NSString, NSObject>)).ToArray();
            dict = keys.Zip(values, (k, v) => new { Key = k, Value = v })
                                    .ToDictionary(x => x.Key, x => x.Value);
            return dict;
        }



        public static Dictionary<string, Dictionary<string, object>[]> ToDictionaryFromNSArray(NSDictionary<NSString, NSObject> dictionary)
        {
            Dictionary<string, Dictionary<string, object>[]> dict = new();
            if (dictionary.Values.Length > 0)
            {
                NSObject first = dictionary.Values[0];
                if (first is NSDictionary tempDict)
                {
                    throw new Exception($"Value type of passed NSDictionary is {first.GetType()}." +
                        $" Use for this type '{"ToDictionaryFromNSDictionary"}'!");
                }
            }

            foreach (KeyValuePair<NSObject, NSObject> pair in dictionary)
            {
                if (pair.Key is NSString key)
                {
                    string strKey = key.ToString();
                    if (pair.Value is NSArray array)
                    {
                        List<Dictionary<string, object>> List = new();
                        for (nuint i = 0; i < array.Count; i++)
                        {
                            NSDictionary<NSString, NSObject> single = array.GetItem<NSDictionary<NSString, NSObject>>(i);
                            if (single != null)
                                List.Add(ToDictionaryFromNSObject(single));
                        }
                        dict.Add(strKey, List.ToArray());
                    }
                }
            }
            return dict;
        }

        public static NSDictionary<NSString, NSObject> ToNSDictionary(Dictionary<string, object> dictionary)
        {
            return NSDictionary<NSString, NSObject>.FromObjectsAndKeys(dictionary.Values.ToArray()
                                                          , dictionary.Keys.ToArray());
        }

        public static NSDictionary<NSString, NSObject> ToNSDictionary(
            Dictionary<string, Dictionary<string, object>[]> dictionary)
        {
            return NSDictionary<NSString, NSObject>.FromObjectsAndKeys(
                dictionary.Values.Select(dicts =>
                NSArray.FromObjects(dicts.Select(dict => ToNSDictionary(dict)).ToArray())).ToArray()
                    , dictionary.Keys.ToArray());
        }

        public static NSDictionary<NSString, NSObject> ToNSDictionary(
            Dictionary<string, Dictionary<string, object>> dictionary)
        {
            return NSDictionary<NSString, NSObject>.FromObjectsAndKeys(dictionary.Values.ToArray()
                                                          , dictionary.Keys.ToArray());
        }
    }
}
#endif