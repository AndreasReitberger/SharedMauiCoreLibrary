namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IAppSettings
    {
        public void Save();
        public void Load();
        public void Reset();
    }
}
