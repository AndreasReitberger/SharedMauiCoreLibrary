using System.Collections;
using System.Globalization;
using System.Resources;

namespace AndreasReitberger.Shared.Core.Extensions
{
    public static class ResourceManagerExtensions
    {
        /// <summary>
        /// Gets the items count of a ResourceFile (.resx)
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static int GetLanguageKeyCount(this ResourceManager resourceManager, CultureInfo culture)
        {
            int count = 0;
            ResourceSet? resourceSet = resourceManager.GetResourceSet(culture, createIfNotExists: true, tryParents: false);

            IDictionaryEnumerator? enumerator = resourceSet?.GetEnumerator();
            while (enumerator?.MoveNext() ?? false) { count++; }
            return count;
        }

        /// <summary>
        /// Compares the item count of two different langauges for a ResourceFile (.resx) and returns the percantage value.
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="culture"></param>
        /// <param name="baseCulture"></param>
        /// <returns></returns>
        public static double GetPercantageTranslationProgress(this ResourceManager resourceManager, CultureInfo culture, CultureInfo? baseCulture = null)
        {
            baseCulture ??= new CultureInfo("en-US");

            int baseCount = GetLanguageKeyCount(resourceManager, baseCulture);
            int count = GetLanguageKeyCount(resourceManager, culture);
            if (count == 0 || baseCount == 0) return 0;

            double percentage = Math.Round(((double)count / (double)baseCount) * 100d, 0);
            return percentage > 100 ? 100 : percentage;
        }
    }
}
