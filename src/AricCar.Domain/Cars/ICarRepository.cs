using AricCar.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace AricCar.Cars
{
    public interface ICarRepository : IRepository<Car, Guid>
    {

    }
}
