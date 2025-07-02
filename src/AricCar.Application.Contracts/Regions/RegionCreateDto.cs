using System.ComponentModel.DataAnnotations;

namespace AricCar.Regions
{
    public class RegionCreateDto
    {
        [Required]
        [StringLength(RegionConsts.RegionCodeMaxLength, MinimumLength = RegionConsts.RegionCodeMinLength)]
        public string ProvincialCode { get; set; }

      
        public string ProvincialName { get; set; }

        [Required]
        [StringLength(RegionConsts.RegionCodeMaxLength, MinimumLength = RegionConsts.RegionCodeMinLength)]
        public string CityCode { get; set; }

      
        public string CityName { get; set; }

        [Required]
        [StringLength(RegionConsts.RegionCodeMaxLength, MinimumLength = RegionConsts.RegionCodeMinLength)]
        public string DistrictCode { get; set; }

       
        public string DistrictName { get; set; }
    }
}
