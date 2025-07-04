using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace AricCar.Cars
{
    public class CarDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ProvincialCode { get; set; }

        public string ProvincialName { get; set; }

        public string CityCode { get; set; }

        public string CityName { get; set; }

        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public string ImageUrl { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
