using AndreasReitberger.Shared.Core.Dispatch;
using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Models.Theme;
using AndreasReitberger.Shared.Core.NavigationManager;
using AndreasReitberger.Shared.Core.Theme;
using System.Text.Json.Serialization;

namespace AndreasReitberger.Shared.Core.SourceGeneration
{
    [JsonSerializable(typeof(DispatchManager))]
    [JsonSerializable(typeof(DispatchErrorEventArgs))]
    [JsonSerializable(typeof(NavigationDoneEventArgs))]
    [JsonSerializable(typeof(NavigationErrorEventArgs))]
    [JsonSerializable(typeof(ShellErrorEventArgs))]
    [JsonSerializable(typeof(ShellNavigator))]
    [JsonSerializable(typeof(ColorInfo))]
    [JsonSerializable(typeof(DefaultThemeManager))]
    [JsonSerializable(typeof(ThemeColorInfo))]
    [JsonSerializable(typeof(ColorPickerElement))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class MauiSourceGenerationContext : JsonSerializerContext { }

}
