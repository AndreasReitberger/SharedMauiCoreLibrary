namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    public struct WooSoftwareLicenseAction
    {
        public const string Activate = "activate";
        public const string Deactivate = "deactivate";
        public const string StatusCheck = "status-check";
        public const string PluginUpdate = "plugin_update";
        public const string PluginInformation = "plugin_information";
        public const string ThemeUpdate = "theme_update";
        public const string CodeVersion = "code_version";
        public const string DeleteKey = "key_delete";

        public WooSoftwareLicenseAction()
        {
        }
    }
}
