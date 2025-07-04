using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AricCar.Cars
{
    public static class CarConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Region." : string.Empty);
        }

        public const int MaxBranchLength = 100;
        public const int MinBranchLength = 1;

        public const int MaxTypeLength = 100;
        public const int MinTypeLength = 1;

        public const int MaxImageUrlLength = 2000;
        public const int MaxDescriptionLength = 4000;
    }
}
