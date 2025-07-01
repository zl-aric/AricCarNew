using AricCar.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace AricCar.Regions
{
    [Authorize(AricCarPermissions.Regions.Default)]
    public class RegionAppService : AricCarAppService, IRegionAppService
    {
        protected IRegionRepository _regionRepository;
        protected RegionManager _regionManager;

        public RegionAppService(IRegionRepository regionRepository, RegionManager regionManager)
        {
            _regionRepository = regionRepository;
            _regionManager = regionManager;
        }

        public async Task<RegionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Region, RegionDto>(await _regionRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<RegionDto>> GetListAsync(GetRegionsInput input)
        {
            var totalCount = await _regionRepository.GetCountAsync(input.ProvincialName, input.CityName, input.DistrictName);
            var items = await _regionRepository.GetListAsync(input.ProvincialName, input.CityName, input.DistrictName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RegionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Region>, List<RegionDto>>(items)
            };
        }

        [Authorize(AricCarPermissions.Regions.Create)]
        public async Task<RegionDto> CreateAsync(RegionCreateDto input)
        {
            var engineInstance = await _regionManager.CreateAsync(input.ProvincialCode, input.ProvincialName, input.CityCode, input.CityName, input.DistrictCode, input.DistrictName);

            return ObjectMapper.Map<Region, RegionDto>(engineInstance);
        }

        [Authorize(AricCarPermissions.Regions.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _regionRepository.DeleteAsync(id);
        }

        [Authorize(AricCarPermissions.Regions.Edit)]
        public async Task<RegionDto> UpdateAsync(Guid id, RegionUpdateDto input)
        {
            var engineInstance = await _regionManager.UpdateAsync(id, input.ProvincialCode, input.ProvincialName, input.CityCode, input.CityName, input.DistrictCode, input.DistrictName, input.ConcurrencyStamp);

            return ObjectMapper.Map<Region, RegionDto>(engineInstance);
        }
    }
}