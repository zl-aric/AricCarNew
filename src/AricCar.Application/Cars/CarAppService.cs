using AricCar.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AricCar.Cars
{
    [Authorize(AricCarPermissions.Cars.Default)]
    public class CarAppService : AricCarAppService, ICarAppService
    {

        private ICarRepository _carRepository;
        private CarManager _carManager;

        public CarAppService(ICarRepository carRepository, CarManager carManager)
        {
            _carRepository = carRepository;
            _carManager = carManager;
        }

        [Authorize(AricCarPermissions.Cars.Create)]
        public async Task<CarDto> CreateAsync(CarCreateDto input)
        {
            var item = await _carManager.CreateAsync(input.DistrictCode, input.Brand, input.Type, input.Description, input.Images);
            return ObjectMapper.Map<Car, CarDto>(item);
        }
    }
}
