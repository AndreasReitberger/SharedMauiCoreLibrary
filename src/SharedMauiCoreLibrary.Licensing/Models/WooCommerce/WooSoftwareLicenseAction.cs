namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    public struct WooSoftwareLicenseAction
    {
        public static string Activate = "activate";
        public static string Deactivate = "deactivate";
        public static string StatusCheck = "status-check";
        public static string PluginUpdate = "plugin_update";
        public static string PluginInformation = "plugin_information";
        public static string ThemeUpdate = "theme_update";
        public static string CodeVersion = "code_version";
        public static string DeleteKey = "key_delete";

        public WooSoftwareLicenseAction()
        {
        }
    }
}
