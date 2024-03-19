using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public class PhysicalUnitFactorConverter
    {
        public static Dictionary<PhysicalUnit, double> UnitFactors = new()
        {
            {PhysicalUnit.g, 1 },
            {PhysicalUnit.kg, 1000 },
            {PhysicalUnit.T, 1000000 },
            {PhysicalUnit.cl, 0.1f },
            {PhysicalUnit.ml, 1 },
            {PhysicalUnit.l, 1000 },
        };
        public static double GetUnitFactor(PhysicalUnit unit)
        {
            if (UnitFactors.ContainsKey(unit))
                return UnitFactors[unit];
            else return 1;
        }
    }
}