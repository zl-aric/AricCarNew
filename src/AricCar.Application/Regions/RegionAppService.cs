using AricCar.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
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
            var totalCount = await _regionRepository.GetCountAsync(input.FilterText, input.ProvinceCode, input.CityCode, input.DistrictCode);
            var items = await _regionRepository.GetListAsync(input.FilterText, input.ProvinceCode, input.CityCode, input.DistrictCode, input.Sorting, input.MaxResultCount, input.SkipCount);

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


        public async Task<List<RegionItem>> GetRegionJsonListAsync()
        {
            var jsonStr = await File.ReadAllTextAsync("wwwroot/region.json");
            var regions = System.Text.Json.JsonSerializer.Deserialize<List<RegionItem>>(jsonStr);
            return regions != null ? regions : new List<RegionItem>();
        }
    }
}