using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Models.DTO
{
    public partial class ErrorDto : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        public partial string Type { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string? StackTrace { get; set; }

        [ObservableProperty]
        public partial ErrorDto? InnerError { get; set; }

        #endregion

        #region Ctor
        public ErrorDto() { }
        public ErrorDto(string type, string message, string? stackTrace, ErrorDto? innerError) : this()
        {
            Type = type;
            Message = message;
            StackTrace = stackTrace;
            InnerError = innerError;
        }

        #endregion
    }
}
