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
            string? provinceName = null, 
            string? cityName = null, 
            string? districtName = null, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), provinceName, cityName, districtName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Region>> GetListAsync(
            string? provinceName = null, 
            string? cityName = null, 
            string? districtName = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), provinceName, cityName, districtName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RegionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Region> ApplyFilter(
                IQueryable<Region> query,
                string? provinceName = null,
                string? cityName = null,
                string? districtName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(provinceName), e => e.ProvincialName.Contains(provinceName!))
                    .WhereIf(!string.IsNullOrWhiteSpace(cityName), e => e.CityName.Contains(cityName!))
                    .WhereIf(!string.IsNullOrWhiteSpace(districtName), e => e.DistrictName.Contains(districtName!));
        }
    }
}
