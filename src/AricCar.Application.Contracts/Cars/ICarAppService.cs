using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AricCar.Cars
{
    public interface ICarAppService : IApplicationService
    {
        Task<CarDto> CreateAsync(CarCreateDto input);
    }
}
