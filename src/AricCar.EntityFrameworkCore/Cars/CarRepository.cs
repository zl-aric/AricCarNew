using AricCar.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AricCar.Cars
{
    public class CarRepository : EfCoreRepository<AricCarDbContext, Car, Guid>, ICarRepository
    {
        public CarRepository(IDbContextProvider<AricCarDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
