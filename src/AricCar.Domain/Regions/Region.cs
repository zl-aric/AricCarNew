using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AricCar.Regions
{
    public class Region : FullAuditedAggregateRoot<Guid>
    {
        public Region()
        {
        }

        public Region(Guid id, string provincialCode, string provincialName, string? cityCode = null, string? cityName = null, string? districtCode = null, string? districtName = null)
        {
            this.Id = id;
            this.ProvincialCode = provincialCode;
            this.ProvincialName = provincialName;
            this.CityCode = cityCode;
            this.CityName = cityName;
            this.DistrictCode = districtCode;
            this.DistrictName = districtName;
        }

        [NotNull]
        public virtual string ProvincialCode { get; set; }

        [NotNull]
        public virtual string ProvincialName { get; set; }

        [CanBeNull]
        public virtual string? CityCode { get; set; }

        [CanBeNull]
        public virtual string? CityName { get; set; }

        [CanBeNull]
        public virtual string? DistrictCode { get; set; }

        [CanBeNull]
        public virtual string? DistrictName { get; set; }
    }
}
