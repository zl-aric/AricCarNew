using System.Collections.Generic;

namespace AricCar.Regions
{
    public class RegionItem
    {
        public string code { get; set; }

        public string name { get; set; }

        public List<RegionItem> children { get; set; }
    }
}
