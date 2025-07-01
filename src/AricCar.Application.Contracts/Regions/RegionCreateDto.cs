using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AricCar.Regions
{
    public class RegionCreateDto
    {
        [Required]
        [StringLength(RegionConsts.RegionCodeMaxLength, MinimumLength = RegionConsts.RegionCodeMinLength)]
        public string ProvincialCode { get; set; }

        [Required]
        [StringLength(RegionConsts.RegionNameMaxLength, MinimumLength = RegionConsts.RegionCodeMinLength)]
        public string ProvincialName { get; set; }

        [StringLength(RegionConsts.RegionCodeMaxLength)]
        public string? CityCode { get; set; }

        [StringLength(RegionConsts.RegionNameMaxLength)]
        public string? CityName { get; set; }

        [StringLength(RegionConsts.RegionCodeMaxLength)]
        public string? DistrictCode { get; set; }

        [StringLength(RegionConsts.RegionNameMaxLength)]
        public string? DistrictName { get; set; }
    }
}
