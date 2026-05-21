using AndreasReitberger.Shared.Core.Chart;
using AndreasReitberger.Shared.Core.Chat;
using AndreasReitberger.Shared.Core.Contacts;
using AndreasReitberger.Shared.Core.Documentation;
using AndreasReitberger.Shared.Core.DTO;
using AndreasReitberger.Shared.Core.EventLogger;
using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Localization;
using AndreasReitberger.Shared.Core.Messaging;
using AndreasReitberger.Shared.Core.Tasks;
using AndreasReitberger.Shared.Core.Update;

namespace AndreasReitberger.Shared.Core.SourceGeneration
{
    [JsonSerializable(typeof(ChartItem))]
    [JsonSerializable(typeof(ChartListItem))]
    [JsonSerializable(typeof(ChartValueDateTimeItem))]
    [JsonSerializable(typeof(ChartValueTimeItem))]
    [JsonSerializable(typeof(WaterfallChartListItem))]
    [JsonSerializable(typeof(ChatMessage))]
    [JsonSerializable(typeof(Contact))]
    [JsonSerializable(typeof(ChangeInfo))]
    [JsonSerializable(typeof(LibraryInfo))]
    [JsonSerializable(typeof(ProVersionFeature))]
    [JsonSerializable(typeof(ResourceInfo))]
    [JsonSerializable(typeof(TutorialStep))]
    [JsonSerializable(typeof(VersionInfo))]
    [JsonSerializable(typeof(ErrorDto))]
    [JsonSerializable(typeof(AppErrorEvent))]
    [JsonSerializable(typeof(AppEvent))]
    [JsonSerializable(typeof(AppInfoEvent))]
    [JsonSerializable(typeof(AppWarningEvent))]
    [JsonSerializable(typeof(LanguageChangedEventArgs))]
    [JsonSerializable(typeof(LocalizationInfo))]
    [JsonSerializable(typeof(LocalizationManager))]
    [JsonSerializable(typeof(StateChangedMessage))]
    [JsonSerializable(typeof(ProjectTask))]
    [JsonSerializable(typeof(UpdateAvailableArgs))]
    [JsonSerializable(typeof(UpdateManager))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class CoreSourceGenerationContext : JsonSerializerContext { }
   
}
