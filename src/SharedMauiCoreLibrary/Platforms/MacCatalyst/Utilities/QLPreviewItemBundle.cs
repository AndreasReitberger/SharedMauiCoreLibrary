#if __IOS__ || __MACCATALYST__
using Foundation;
using QuickLook;

namespace AndreasReitberger.Shared.Core.Platforms.MacCatalyst.Utilities
{
    public class QLPreviewItemBundle : QLPreviewItem
    {
        readonly string _fileName, _filePath;
        public QLPreviewItemBundle(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public override string PreviewItemTitle
        {
            get
            {
                return _fileName;
            }
        }
        public override NSUrl PreviewItemUrl
        {
            get
            {
                string documents = NSBundle.MainBundle.BundlePath;
                string lib = Path.Combine(documents, _filePath);
                NSUrl url = NSUrl.FromFilename(lib);
                return url;
            }
        }
    }
}
#endif