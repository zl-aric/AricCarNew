using AricCar.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AricCar.Regions
{
    public class EfCoreRegionRepository : EfCoreRepository<AricCarDbContext, Region, Guid>, IRegionRepository
    {
        public EfCoreRegionRepository(IDbContextProvider<AricCarDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(
            string? filter = null,
            string? provinceCode = null,
            string? cityCode = null,
            string? districtCode = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()),filter, provinceCode, cityCode, districtCode);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Region>> GetListAsync(
            string? filter = null,
            string? provinceCode = null,
            string? cityCode = null,
            string? districtCode = null,
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filter, provinceCode, cityCode, districtCode);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RegionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Region> ApplyFilter(
                IQueryable<Region> query,
                string? filter = null,
                string? provinceCode = null,
                string? cityCode = null,
                string? districtCode = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.ProvincialName!.Contains(filter!)|| e.CityName!.Contains(filter!)|| e.DistrictName!.Contains(filter!))
                    .WhereIf(!string.IsNullOrWhiteSpace(provinceCode), e => e.ProvincialCode == provinceCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(cityCode), e => e.CityCode == cityCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(districtCode), e => e.DistrictCode == districtCode);
        }
    }
}
