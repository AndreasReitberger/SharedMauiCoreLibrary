using System.Reflection;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class UserSecretsManager
    {
        public class UserSecretsManagerBuilder
        {
            #region Instance
            readonly UserSecretsManager _manager = new();
            #endregion

            #region Methods

            public UserSecretsManager Build()
            {
                _manager.Initialize();
                return _manager;
            }

            public UserSecretsManagerBuilder WithCustomJsonFileName(string fileName)
            {
                _manager.UserSecretsFileName = fileName;
                return this;
            }

            public UserSecretsManagerBuilder WithCustomAssambly(Assembly assembly)
            {
                _manager.CurrentAssembly = assembly;
                return this;
            }

            public UserSecretsManagerBuilder WithAppNamespace(string appNamespace)
            {
                _manager.AppNamespace = appNamespace;
                return this;
            }

            #endregion
        }
    }
}