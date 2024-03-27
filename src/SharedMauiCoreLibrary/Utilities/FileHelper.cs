using CommunityToolkit.Maui.Storage;

namespace AndreasReitberger.Shared.Core.Utilities
{
    /// <summary>
    /// A helper class to save and open files
    /// Source
    /// - 1: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/essentials/file-saver?tabs=windows
    /// - 2: https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/launcher?view=net-maui-7.0&tabs=macios#open-another-app-via-a-file
    /// </summary>
    public static class FileHelper
    {
        #region Properties
        // https://wiki.selfhtml.org/wiki/MIME-Type/%C3%9Cbersicht
        public static string ContentType_Plain = "text/plain";
        public static string ContentType_JSON = "application/json";
        public static string ContentType_PDF = "application/pdf";

        #endregion

        #region Methods
        /// <summary>
        /// Saves a file with the provided file name.
        /// </summary>
        /// <param name="saver">The <c>FileSaver</c> isntance</param>
        /// <param name="file">The file name</param>
        /// <param name="fileStream">The file stream, for instanc a <c>MemoryStream</c></param>
        /// <param name="initialPath">The initial folder</param>
        /// <param name="ct">A cancellation token</param>
        /// <returns></returns>
        public static async Task<FileSaverResult> SaveFileAsync(
            IFileSaver saver, string file, Stream fileStream, string? initialPath = null, CancellationToken ct = default
            )
        {
            return string.IsNullOrEmpty(initialPath)
                ? await saver.SaveAsync(file, fileStream, ct)
                : await saver.SaveAsync(initialPath, file, fileStream, ct);
        }

        /// <summary>
        /// Opens the provided <c>OpenFileRequest</c>
        /// </summary>
        /// <param name="launcher">The <c>Launcher</c> instance</param>
        /// <param name="openFileRequest">The target <c>OpenFileRequest</c></param>
        /// <returns></returns>
        public static Task<bool> OpenFileAsync(ILauncher launcher, OpenFileRequest openFileRequest) => launcher.OpenAsync(openFileRequest);

        /// <summary>
        /// Opens the provided file name and content type
        /// </summary>
        /// <param name="launcher">The <c>Launcher</c> instance</param>
        /// <param name="title">The title for the dialog</param>
        /// <param name="filePath">The full file path</param>
        /// <param name="contentType">The contentType</param>
        /// <returns></returns>
        public static Task<bool> OpenFileAsync(ILauncher launcher, string title, string filePath, string contentType)
            => OpenFileAsync(launcher, new OpenFileRequest() { Title = title, File = new(filePath, contentType) });

        /// <summary>
        /// Saves and opens the saved file on success. Otherwise it returns false.
        /// </summary>
        /// <param name="saver">The <c>FileSaver</c> isntance</param>
        /// <param name="file">The file name</param>
        /// <param name="fileStream">The file stream, for instanc a <c>MemoryStream</c></param>
        /// <param name="initialPath">The initial folder</param>
        /// <param name="launcher">The <c>Launcher</c> instance</param>
        /// <param name="title">The title for the dialog</param>
        /// <param name="filePath">The full file path</param>
        /// <param name="contentType">The contentType</param>
        /// <param name="ct">A cancellation token</param>
        /// <returns><c>true</c> if the file was saved successfully</returns>
        public static async Task<bool> SaveAndOpenFileAsync(
            IFileSaver saver, string file, Stream fileStream,
            ILauncher launcher, string title, string? initialPath = null, string contentType = "text/plain",
            CancellationToken ct = default
            )
        {
            FileSaverResult result = await SaveFileAsync(saver, file, fileStream, initialPath, ct).ConfigureAwait(false);
            if (result?.IsSuccessful is true)
                return await OpenFileAsync(launcher, new OpenFileRequest() { Title = title, File = new(result.FilePath, contentType) }).ConfigureAwait(false);
            else return false;
        }
        #endregion
    }
}
