using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AricCar.Regions
{
    public interface IRegionAppService : IApplicationService
    {
        Task<PagedResultDto<RegionDto>> GetListAsync(GetRegionsInput input);

        Task<RegionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RegionDto> CreateAsync(RegionCreateDto input);

        Task<List<RegionItem>> GetRegionJsonListAsync();
    }
}
