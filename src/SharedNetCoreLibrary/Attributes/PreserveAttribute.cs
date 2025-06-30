namespace AndreasReitberger.Shared.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}
