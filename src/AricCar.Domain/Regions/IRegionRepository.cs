using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AricCar.Regions
{
    public interface IRegionRepository : IRepository<Region, Guid>
    {
        Task<List<Region>> GetListAsync(
           string? provinceName = null,
           string? cityName = null,
           string? districtName = null,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default
       );

        Task<long> GetCountAsync(
            string? provinceName = null,
           string? cityName = null,
           string? districtName = null,
            CancellationToken cancellationToken = default);
    }
}
