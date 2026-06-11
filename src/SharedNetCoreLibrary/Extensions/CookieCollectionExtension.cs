using System.Net;

namespace AndreasReitberger.Shared.Core.Extensions
{
    public static class CookieCollectionExtension
    {
        extension(CookieCollection collection)
        {
            public CookieContainer ToContainer()
            {
                CookieContainer container = new();
                container.Add(collection);
                return container;
            }
        }
    }
}
