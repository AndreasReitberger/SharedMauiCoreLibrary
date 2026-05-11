using System.Reflection;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IUserSecretsManager
    {
        #region Properties
        public string AppNamespace { get; set; }
        public string UserSecretsFileName { get; set; }
        public Assembly? CurrentAssembly { get; set; }
        #endregion

        #region Methods
        public void Initialize(Assembly? assembly = null);
        public T? ToObject<T>(JsonSerializerContext? context = null);
        public T? ReadSection<T>(string sectionName, JsonSerializerContext? context = null);
        public T? ReadSectionFromConfigurationRoot<T>(Type type, string sectionName, JsonSerializerContext? context = null);
        public string this[string name] { get; }
        #endregion

    }
}
