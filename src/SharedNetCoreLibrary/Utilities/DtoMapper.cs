using AndreasReitberger.Shared.Core.Models.DTO;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public static class DtoMapper
    {
        public static ErrorDto? FromException(Exception? ex) =>
                ex is null ? null :
                new ErrorDto(
                    ex.GetType().FullName ?? "System.Exception",
                    ex.Message,
                    ex.StackTrace,
                    FromException(ex.InnerException));

    }
}
