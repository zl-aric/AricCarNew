using Volo.Abp.Application.Dtos;

namespace AricCar.Regions
{
    public class GetRegionsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? ProvinceCode { get; set; }

        public string? CityCode { get; set; }

        public string? DistrictCode { get; set; }
    }
}
