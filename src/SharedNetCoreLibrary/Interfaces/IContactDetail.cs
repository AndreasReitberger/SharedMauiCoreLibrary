using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IContactDetail
    {
        #region Properties

        public ContactType Type { get; set; }
        public string Value { get; set; }

        #endregion
    }
}
