#if __IOS__ || __MACCATALYST__
using QuickLook;

namespace AndreasReitberger.Shared.Core.Platforms.MacCatalyst.Utilities
{
    public class PreviewControllerDS : QLPreviewControllerDataSource
    {
        //Document cache
        private QLPreviewItem _item;

        //Setting the document
        public PreviewControllerDS(QLPreviewItem item)
        {
            _item = item;
        }

        //Setting document count to 1
        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return (nint)1;
        }

        //Return the document
        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return _item;
        }
    }
}
#endif