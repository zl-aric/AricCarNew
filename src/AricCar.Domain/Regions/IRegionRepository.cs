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
            string? filter = null,
           string? provinceCode = null,
           string? cityCode = null,
           string? districtCode = null,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default
       );

        Task<long> GetCountAsync(
            string? filter = null,
            string? provinceCode = null,
           string? cityCode = null,
           string? districtCode = null,
            CancellationToken cancellationToken = default);
    }
}
