namespace AndreasReitberger.Shared.Core.Utilities
{
    public class AppResourceKeyManager
    {
        public static void OverrideKeyValue<T>(ResourceDictionary resources, string key, T newValue)
        {
            Stack<ResourceDictionary> allMergedDicts = new();
            foreach (ResourceDictionary mergedDict in resources.MergedDictionaries)
            {
                allMergedDicts.Push(mergedDict);
            }
            do
            {
                ResourceDictionary currentDictionary = allMergedDicts.Pop();
                if (currentDictionary.MergedDictionaries.Count > 0)
                {
                    foreach (ResourceDictionary mergedDict in currentDictionary.MergedDictionaries)
                    {
                        allMergedDicts.Push(mergedDict);
                    }
                }
                else
                {
                    if (currentDictionary.TryGetValue(key, out object dictVal))
                    {
                        // Check if type matches
                        if (dictVal is T parameter)
                        {
                            currentDictionary[key] = newValue;
                        }
                        else
                        {
                            throw new ArrayTypeMismatchException($"The value for the key '{key}' doesn't match the type of the current value.");
                        }
                    }
                }

            } while (allMergedDicts?.Count > 0);
        }
    }
}
