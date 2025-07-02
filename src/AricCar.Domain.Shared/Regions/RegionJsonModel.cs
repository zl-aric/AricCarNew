using System.Collections.Generic;

namespace AricCar.Regions
{
    public class RegionJsonModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public List<RegionJsonModel> Children { get; set; }
    }
}
