using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace AricCar.Regions
{
    public class RegionManager : DomainService
    {
        protected IRegionRepository _regionRepository;

        public RegionManager(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public virtual async Task<Region> CreateAsync(
            string provincialCode,
            string provincialName,
            string? cityCode = null,
            string? cityName = null,
            string? districtCode = null,
            string? districtName = null)
        {
            Check.NotNullOrWhiteSpace(provincialCode, nameof(provincialCode));
            Check.Length(provincialCode, nameof(provincialCode), RegionConsts.RegionCodeMaxLength, RegionConsts.RegionCodeMinLength);
            Check.NotNullOrWhiteSpace(provincialName, nameof(provincialName));
            Check.Length(provincialName, nameof(provincialName), RegionConsts.RegionNameMaxLength, RegionConsts.RegionNameMinLength);

            Check.Length(cityCode, nameof(cityCode), RegionConsts.RegionCodeMaxLength);
            Check.Length(cityName, nameof(cityName), RegionConsts.RegionNameMaxLength);
            Check.Length(districtCode, nameof(districtCode), RegionConsts.RegionCodeMaxLength);
            Check.Length(districtName, nameof(districtName), RegionConsts.RegionNameMaxLength);

            var isExist = await _regionRepository.AnyAsync(x => x.ProvincialCode == provincialCode && x.DistrictCode == districtCode && x.CityCode == cityCode);
            if (isExist)
            {
                throw new RegionRepeatException();
            }

            var region = new Region(
                GuidGenerator.Create(),
                provincialCode,
                provincialName,
                cityCode,
                cityName,
                districtCode,
                districtName
            );

            return await _regionRepository.InsertAsync(region);
        }

        public virtual async Task<Region> UpdateAsync(
            Guid id,
            string provincialCode,
            string provincialName,
            string? cityCode = null,
            string? cityName = null,
            string? districtCode = null,
            string? districtName = null,
            [CanBeNull] string? concurrencyStamp = null
      )
        {
            Check.NotNullOrWhiteSpace(provincialCode, nameof(provincialCode));
            Check.Length(provincialCode, nameof(provincialCode), RegionConsts.RegionCodeMaxLength, RegionConsts.RegionCodeMinLength);
            Check.NotNullOrWhiteSpace(provincialName, nameof(provincialName));
            Check.Length(provincialName, nameof(provincialName), RegionConsts.RegionNameMaxLength, RegionConsts.RegionNameMinLength);

            Check.Length(cityCode, nameof(cityCode), RegionConsts.RegionCodeMaxLength);
            Check.Length(cityName, nameof(cityName), RegionConsts.RegionNameMaxLength);
            Check.Length(districtCode, nameof(districtCode), RegionConsts.RegionCodeMaxLength);
            Check.Length(districtName, nameof(districtName), RegionConsts.RegionNameMaxLength);

            var isExist = await _regionRepository.AnyAsync(x => x.Id != id && x.ProvincialCode == provincialCode && x.DistrictCode == districtCode && x.CityCode == cityCode);
            if (isExist)
            {
                throw new RegionRepeatException();
            }

            var item = await _regionRepository.GetAsync(id);

            item.ProvincialCode = provincialCode;
            item.ProvincialName = provincialName;
            item.CityCode = cityCode;
            item.CityName = cityName;
            item.DistrictCode = districtCode;
            item.DistrictName = districtName;

            item.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _regionRepository.UpdateAsync(item);
        }
    }
}