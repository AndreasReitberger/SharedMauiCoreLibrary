using CommunityToolkit.Maui.Storage;
using System.Runtime.Versioning;
using System.Text;

namespace AndreasReitberger.Shared.Core.Utilities
{
    /// <summary>
    /// A helper class to save and open files
    /// Source
    /// - 1: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/essentials/file-saver?tabs=windows
    /// - 2: https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/launcher?view=net-maui-8.0
    /// </summary>
    public static class FileHelper
    {
        #region Properties
        // https://wiki.selfhtml.org/wiki/MIME-Type/%C3%9Cbersicht
        public static string ContentType_Plain = "text/plain";
        public static string ContentType_JSON = "application/json";
        public static string ContentType_PDF = "application/pdf";
        public static string ContentType_Excel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

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
        [SupportedOSPlatform("Android26.0")]
        [SupportedOSPlatform("iOS14.0")]
        [SupportedOSPlatform("MacCatalyst14.0")]
        //[SupportedOSPlatform("Tizen")]
        [SupportedOSPlatform("Windows")]
        public static async Task<FileSaverResult> SaveFileAsync(
            IFileSaver? saver, string file, Stream fileStream, string? initialPath = null, CancellationToken ct = default
            )
        {
            saver ??= FileSaver.Default;
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
        public static async Task<bool> ShowFileAsync(ILauncher? launcher, OpenFileRequest openFileRequest)
        {
            launcher ??= Launcher.Default;
            return await launcher.OpenAsync(openFileRequest);
        }

        /// <summary>
        /// Opens the provided file name and content type
        /// </summary>
        /// <param name="launcher">The <c>Launcher</c> instance</param>
        /// <param name="title">The title for the dialog</param>
        /// <param name="filePath">The full file path</param>
        /// <param name="contentType">The contentType</param>
        /// <returns></returns>
        public static Task<bool> ShowFileAsync(ILauncher launcher, string title, string filePath, string contentType)
            => ShowFileAsync(launcher, new OpenFileRequest() { Title = title, File = new(filePath, contentType) });

        /// <summary>
        /// Saves and opens the saved file on success. Otherwise it returns false.
        /// </summary>
        /// <param name="saver">The <c>FileSaver</c> isntance</param>
        /// <param name="file">The file name</param>
        /// <param name="fileStream">The file stream, for instanc a <c>MemoryStream</c></param>
        /// <param name="initialPath">The initial folder</param>
        /// <param name="launcher">The <c>Launcher</c> instance</param>
        /// <param name="title">The title for the dialog</param>
        /// <param name="contentType">The contentType</param>
        /// <param name="ct">A cancellation token</param>
        /// <returns><c>true</c> if the file was saved successfully</returns>
        [SupportedOSPlatform("Android26.0")]
        [SupportedOSPlatform("iOS14.0")]
        [SupportedOSPlatform("MacCatalyst14.0")]
        //[SupportedOSPlatform("Tizen")]
        [SupportedOSPlatform("Windows")]
        public static async Task<bool> SaveAndShowFileAsync(
            IFileSaver? saver, string file, Stream fileStream,
            ILauncher? launcher, string title, string? initialPath = null, string contentType = "text/plain",
            CancellationToken ct = default
            )
        {
            saver ??= FileSaver.Default;
            FileSaverResult result = await SaveFileAsync(saver, file, fileStream, initialPath, ct).ConfigureAwait(false);
            if (result?.IsSuccessful is true)
            {
                launcher ??= Launcher.Default;
                return await ShowFileAsync(launcher, new OpenFileRequest() { Title = title, File = new(result.FilePath, contentType) }).ConfigureAwait(false);
            }
            else return false;
        }

        public static async Task<FileResult?> OpenFileAsync(IFilePicker? picker, string title, FilePickerFileType? types = null)
        {
            picker ??= FilePicker.Default;
            return await picker.PickAsync(new PickOptions()
            {
                PickerTitle = title,
                FileTypes = types,
            });
        }
        #endregion

        #region Helper
        public static string GetDuplicatedFileName(string path, string targetFileName)
        {
            string name = string.Empty;
            string[] parts = targetFileName.Split('_');
            StringBuilder fileName = new();
            for (int i = 0; i < parts.Length - 1; i++)
            {
                fileName.Append(parts[i]);
            }
            IEnumerable<string> duplicates = Directory.GetFiles(path).Where(file => file.StartsWith(fileName.ToString()));
            name = $"{fileName}_{duplicates.Count()}{parts[^1]}";
            return name;
        }
        #endregion
    }
}
