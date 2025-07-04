using System.Threading.Tasks;

namespace AricCar.Cars
{
    public interface ICarAppService
    {
        Task<CarDto> CreateAsync(CarCreateDto input);
    }
}
