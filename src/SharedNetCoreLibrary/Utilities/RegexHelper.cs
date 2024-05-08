using System.Text.RegularExpressions;

namespace AndreasReitberger.Shared.Core.Utilities
{
#if NET7_0_OR_GREATER
    public static partial class RegexHelper
    {
        #region LexOffice & SevDesk

        [GeneratedRegex(@"(^[a-zA-Z0-9]*)")]
        public static partial Regex LexOfficeAccessTokenGeneratedRegex();

        [GeneratedRegex(@"(^[a-zA-Z0-9]*)")]
        public static partial Regex SevDeskAccessTokenGeneratedRegex();

        [Obsolete($"Use {nameof(LexOfficeAccessTokenGeneratedRegex)} instead!")]
        public const string LexOfficeAccessToken = @"(^[a-zA-Z0-9]*)"; //@"(^([0-9A-Za-z]{8}[-][0-9A-Za-z]{4}[-][0-9A-Za-z]{4}[-][0-9A-Za-z]{4}[-][0-9A-Za-z]{12})$)";
        [Obsolete($"Use {nameof(SevDeskAccessTokenGeneratedRegex)} instead!")]
        public const string SevDeskAccessToken = @"(^[a-zA-Z0-9]*)"; //@"(^([a-z0-9])\w+$)";
        #endregion

        #region OctoPrint
        [GeneratedRegex(@"([A-Z]|[0-9])*")]
        public static partial Regex OctoPrintApiKeyGeneratedRegex();

        [Obsolete($"Use {nameof(OctoPrintApiKeyGeneratedRegex)} instead!")]
        public const string OctoPrintApiKey = @"([A-Z]|[0-9])*";
        #endregion

        #region RepetierServerPro
        [GeneratedRegex(@"(\w{8}-\w{4}-\w{4}-\w{4}-\w{12})")]
        public static partial Regex RepetierServerProApiKeyGeneratedRegex();

        [Obsolete($"Use {nameof(RepetierServerProApiKeyGeneratedRegex)} instead!")]
        public const string RepetierServerProApiKey = @"(\w{8}-\w{4}-\w{4}-\w{4}-\w{12})";
        #endregion

        #region Networking
        [GeneratedRegex(@"(?=^.{4,253}$)(^((?!-)[a-zA-Z0-9-]{1,63}(?<!-)\.)+[a-zA-Z]{2,63}$)")]
        public static partial Regex FqdnGeneratedRegex();

        [Obsolete($"Use {nameof(FqdnGeneratedRegex)} instead!")]
        public const string Fqdn = @"(?=^.{4,253}$)(^((?!-)[a-zA-Z0-9-]{1,63}(?<!-)\.)+[a-zA-Z]{2,63}$)";

        const string IPv4AddressValues = @"(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])";
        // Match IPv4-Address like 192.168.178.1
        [GeneratedRegex("^" + IPv4AddressValues + "$")]
        public static partial Regex IPv4AddressValuesGeneratedRegex();

        [Obsolete($"Use {nameof(IPv4AddressValuesGeneratedRegex)} instead!")]
        public const string IPv4AddressRegex = "^" + IPv4AddressValues + "$";

        // Match IPv6-Address
        [GeneratedRegex(@"(?:^|(?<=\s))(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))(?=\s|$)")]
        public static partial Regex IPv6AddressGeneratedRegex();

        [Obsolete($"Use {nameof(IPv6AddressGeneratedRegex)} instead!")]
        public const string IPv6AddressRegex = @"(?:^|(?<=\s))(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))(?=\s|$)";

        // Match IPv4-Address Range like 192.168.178.1-192.168.178.127
        [GeneratedRegex("^" + IPv4AddressValues + "-" + IPv4AddressValues + "$")]
        public static partial Regex IPv4AddressRangeGeneratedRegex();

        [Obsolete($"Use {nameof(IPv4AddressRangeGeneratedRegex)} instead!")]
        public const string IPv4AddressRangeRegex = "^" + IPv4AddressValues + "-" + IPv4AddressValues + "$";

        // Match IPv4-Address within a string
        [GeneratedRegex(@"\d{1,3}(\.\d{1,3}){3}")]
        public static partial Regex IPv4AddressExctractGeneratedRegex();

        [Obsolete($"Use {nameof(IPv4AddressExctractGeneratedRegex)} instead!")]
        public const string IPv4AddressExctractRegex = @"\d{1,3}(\.\d{1,3}){3}";


        // Match a MAC-Address 000000000000 00:00:00:00:00:00, 00-00-00-00-00-00-00 or 0000.0000.0000
        [GeneratedRegex(@"^^[A-Fa-f0-9]{12}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$|^[A-Fa-f0-9]{4}.[A-Fa-f0-9]{4}.[A-Fa-f0-9]{4}$$")]
        public static partial Regex MACAddressGeneratedRegex();

        [Obsolete($"Use {nameof(IPv4AddressExctractGeneratedRegex)} instead!")]
        public const string MACAddressRegex = @"^^[A-Fa-f0-9]{12}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$|^[A-Fa-f0-9]{4}.[A-Fa-f0-9]{4}.[A-Fa-f0-9]{4}$$";

        // Matche the first 3 bytes of a MAC-Address 000000, 00:00:00, 00-00-00
        [GeneratedRegex(@"^[A-Fa-f0-9]{6}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$|^[A-Fa-f0-9]{4}.[A-Fa-f0-9]{2}$")]
        public static partial Regex MACAddressFirst3BytesGeneratedRegex();

        [Obsolete($"Use {nameof(MACAddressFirst3BytesGeneratedRegex)} instead!")]
        public const string MACAddressFirst3BytesRegex = @"^[A-Fa-f0-9]{6}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$|^[A-Fa-f0-9]{4}.[A-Fa-f0-9]{2}$";
        #endregion

        #region Files 
        // Match any filepath --> https://www.codeproject.com/Tips/216238/Regular-Expression-to-Validate-File-Path-and-Exten
        [GeneratedRegex(@"^(?:[\w]\:|\\)(\\[a-z_\-\s0-9\.]+)+\.[a-zA-z0-9]{1,4}$")]
        public static partial Regex AnyFilePathGeneratedRegex();

        [Obsolete($"Use {nameof(AnyFilePathGeneratedRegex)} instead!")]
        public const string FilePath = @"^(?:[\w]\:|\\)(\\[a-z_\-\s0-9\.]+)+\.[a-zA-z0-9]{1,4}$";
        #endregion

        #region HTML
        // https://stackoverflow.com/questions/18153998/how-do-i-remove-all-html-tags-from-a-string-without-knowing-which-tags-are-in-it
        [GeneratedRegex(@"<.*?>")]
        public static partial Regex HtmlTagsGeneratedRegex();

        [Obsolete($"Use {nameof(HtmlTagsGeneratedRegex)} instead!")]
        public const string HtmlTags = @"<.*?>"; // <[a-zA-Z/]*?>
        #endregion
    }
#else
    public static partial class RegexHelper
    {
        #region LexOffice & SevDesk

        public const string LexOfficeAccessToken = @"(^[a-zA-Z0-9]*)"; //@"(^([0-9A-Za-z]{8}[-][0-9A-Za-z]{4}[-][0-9A-Za-z]{4}[-][0-9A-Za-z]{4}[-][0-9A-Za-z]{12})$)";

        public const string SevDeskAccessToken = @"(^[a-zA-Z0-9]*)"; //@"(^([a-z0-9])\w+$)";
        #endregion

        #region OctoPrint
        public const string OctoPrintApiKey = @"([A-Z]|[0-9])*";
        #endregion

        #region RepetierServerPro

        public const string RepetierServerProApiKey = @"(\w{8}-\w{4}-\w{4}-\w{4}-\w{12})";
        #endregion

        #region Networking

        public const string Fqdn = @"(?=^.{4,253}$)(^((?!-)[a-zA-Z0-9-]{1,63}(?<!-)\.)+[a-zA-Z]{2,63}$)";

        const string IPv4AddressValues = @"(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])";
        
        // Match IPv4-Address like 192.168.178.1
        public const string IPv4AddressRegex = "^" + IPv4AddressValues + "$";

        // Match IPv6-Address
        public const string IPv6AddressRegex = @"(?:^|(?<=\s))(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))(?=\s|$)";

        // Match IPv4-Address Range like 192.168.178.1-192.168.178.127
        public const string IPv4AddressRangeRegex = "^" + IPv4AddressValues + "-" + IPv4AddressValues + "$";

        // Match IPv4-Address within a string
        public const string IPv4AddressExctractRegex = @"\d{1,3}(\.\d{1,3}){3}";


        // Match a MAC-Address 000000000000 00:00:00:00:00:00, 00-00-00-00-00-00-00 or 0000.0000.0000
        public const string MACAddressRegex = @"^^[A-Fa-f0-9]{12}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$|^[A-Fa-f0-9]{4}.[A-Fa-f0-9]{4}.[A-Fa-f0-9]{4}$$";

        // Matche the first 3 bytes of a MAC-Address 000000, 00:00:00, 00-00-00
        public const string MACAddressFirst3BytesRegex = @"^[A-Fa-f0-9]{6}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$|^[A-Fa-f0-9]{4}.[A-Fa-f0-9]{2}$";
        #endregion

        #region Files 

        public const string FilePath = @"^(?:[\w]\:|\\)(\\[a-z_\-\s0-9\.]+)+\.[a-zA-z0-9]{1,4}$";
        #endregion

        #region HTML

        public const string HtmlTags = @"<.*?>"; // <[a-zA-Z/]*?>
        #endregion
    }
#endif
}
