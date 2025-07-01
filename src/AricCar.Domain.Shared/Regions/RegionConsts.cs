namespace AricCar.Regions
{
    public static class RegionConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";
        public const int RegionCodeMinLength = 2;
        public const int RegionCodeMaxLength = 10;
        public const int RegionNameMinLength = 2;
        public const int RegionNameMaxLength = 100;
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Region." : string.Empty);
        }
    }
}
