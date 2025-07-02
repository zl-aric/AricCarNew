using System;
using System.Collections.Generic;

namespace AricCar.Regions
{
    public class RegionItem : IEquatable<RegionItem>
    {
        public string code { get; set; }

        public string name { get; set; }

        public List<RegionItem> children { get; set; }

        public bool Equals(RegionItem? other)
        {
            if (other is null) return false;
            return code == other.code && name == other.name;
        }

        public override bool Equals(object obj) => Equals(obj as RegionItem);

        public override int GetHashCode() => HashCode.Combine(code, name);
    }
}
