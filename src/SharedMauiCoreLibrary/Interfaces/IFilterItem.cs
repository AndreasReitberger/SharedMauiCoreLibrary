namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IFilterItem
    {
        #region Properties
        public Type TargetType { get; set; } 
        public string SearchText { get; set; }

        #endregion
    }
}
