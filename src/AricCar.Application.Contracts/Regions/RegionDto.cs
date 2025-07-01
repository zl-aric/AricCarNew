using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace AricCar.Regions
{
    public class RegionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ProvincialCode { get; set; }

        public string ProvincialName { get; set; }

        public string? CityCode { get; set; }

        public string? CityName { get; set; }

        public string? DistrictCode { get; set; }

        public string? DistrictName { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}