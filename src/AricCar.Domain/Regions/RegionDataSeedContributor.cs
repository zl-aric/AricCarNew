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
            var regions = System.Text.Json.JsonSerializer.Deserialize<List<RegionItem>>(jsonStr);
            var gdProvince = regions!.Where(x => x.name == "广东省").FirstOrDefault();
            var szCity = gdProvince!.children.Where(x => x.name == "深圳市").FirstOrDefault();
            foreach (var item in szCity!.children)
            {
                var region = new Region(_guidGenerator.Create(), gdProvince.code, gdProvince.name, szCity.code, szCity.name, item.code, item.name);
                await _repository.InsertAsync(region);
            }
        }
    }
}
