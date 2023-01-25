namespace AndreasReitberger.Shared.Core.Services
{
    public partial class ImageResizerService
    {
        public partial byte[] ResizeImage(byte[] imageData, float width, float height = -1)
        {
            ImageSource image = ImageSource.FromStream(() => new MemoryStream(imageData));
            throw new NotImplementedException("Not implemented yet!");
            //return imageData;
        }
    }
}
