using Volo.Abp.Application.Dtos;

namespace AricCar.Regions
{
    public class GetRegionsInput : PagedAndSortedResultRequestDto
    {

        public string? ProvincialName { get; set; }

        public string? CityName { get; set; }

        public string? DistrictName { get; set; }
    }
}
