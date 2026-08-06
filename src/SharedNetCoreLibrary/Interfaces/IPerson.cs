namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IPerson
    {
        #region Properties

        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IAddress? Address { get; set; }
        public ICollection<IContactDetail> ContactDetails { get; set; }

        #endregion
    }
}
