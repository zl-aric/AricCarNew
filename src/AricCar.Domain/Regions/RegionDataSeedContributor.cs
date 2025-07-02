using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace AricCar.Regions
{
    public class RegionDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Region, Guid> _repository;
        private readonly IGuidGenerator _guidGenerator;

        public RegionDataSeedContributor(
            IRepository<Region, Guid> bookRepository,
            IGuidGenerator guidGenerator)
        {
            _repository = bookRepository;
            _guidGenerator = guidGenerator;
        }


        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _repository.GetCountAsync() > 0)
                return;
            var jsonStr = await File.ReadAllTextAsync("wwwroot/region.json");
            var regions = System.Text.Json.JsonSerializer.Deserialize<List<RegionJsonModel>>(jsonStr);
            var gdProvince = regions!.Where(x => x.Name == "广东省").FirstOrDefault();
            var szCity = gdProvince!.Children.Where(x => x.Name == "深圳市").FirstOrDefault();
            // 广东省
            var region = new Region(_guidGenerator.Create(), gdProvince.Code, gdProvince.Name);
            await _repository.InsertAsync(region);
            // 广东省深圳市
            region = new Region(_guidGenerator.Create(), gdProvince.Code, gdProvince.Name, szCity.Code, szCity.Name);
            await _repository.InsertAsync(region);
            foreach (var item in szCity!.Children)
            {
                region = new Region(_guidGenerator.Create(), gdProvince.Code, gdProvince.Name, szCity.Code, szCity.Name, item.Code, item.Name);
                await _repository.InsertAsync(region);
            }

            throw new NotImplementedException();
        }
    }
}
