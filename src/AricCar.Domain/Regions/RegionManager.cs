using System.Threading.Tasks;
using Volo.Abp;
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
            string cityCode,
            string cityName,
            string districtCode,
            string districtName)
        {
            Check.NotNullOrWhiteSpace(provincialCode, nameof(provincialCode));
            Check.Length(provincialCode, nameof(provincialCode), RegionConsts.RegionCodeMaxLength, RegionConsts.RegionCodeMinLength);
            Check.NotNullOrWhiteSpace(provincialName, nameof(provincialName));
            Check.Length(provincialName, nameof(provincialName), RegionConsts.RegionNameMaxLength, RegionConsts.RegionNameMinLength);
            Check.NotNullOrWhiteSpace(cityCode, nameof(cityCode));
            Check.Length(cityCode, nameof(cityCode), RegionConsts.RegionCodeMaxLength, RegionConsts.RegionCodeMinLength);
            Check.NotNullOrWhiteSpace(cityName, nameof(cityName));
            Check.Length(cityName, nameof(cityName), RegionConsts.RegionNameMaxLength, RegionConsts.RegionNameMinLength);
            Check.NotNullOrWhiteSpace(districtCode, nameof(districtCode));
            Check.Length(districtCode, nameof(districtCode), RegionConsts.RegionCodeMaxLength, RegionConsts.RegionCodeMinLength);
            Check.NotNullOrWhiteSpace(districtName, nameof(districtName));
            Check.Length(districtName, nameof(districtName), RegionConsts.RegionNameMaxLength, RegionConsts.RegionNameMinLength);

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
    }
}