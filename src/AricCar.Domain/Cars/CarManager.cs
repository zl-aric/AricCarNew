using AricCar.Regions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace AricCar.Cars
{
    public class CarManager : DomainService
    {
        private readonly ICarRepository _carRepository;

        public CarManager(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public virtual async Task<Car> CreateAsync(
            string DistrctCode,
            string Brand,
            string Type,
            string? Description,
            List<string> images)
        {
            Check.NotNullOrWhiteSpace(DistrctCode, nameof(DistrctCode));
            Check.Length(DistrctCode, nameof(DistrctCode), RegionConsts.RegionCodeMaxLength, RegionConsts.RegionCodeMinLength);
            Check.NotNullOrWhiteSpace(Brand, nameof(Brand));
            Check.Length(Brand, nameof(Brand), CarConsts.MaxBranchLength, CarConsts.MinBranchLength);
            Check.NotNullOrWhiteSpace(Type, nameof(Type));
            Check.Length(Type, nameof(Type), CarConsts.MaxTypeLength, CarConsts.MinTypeLength);
            Check.Length(Description, nameof(Description), CarConsts.MaxDescriptionLength);
            Check.NotNullOrEmpty(images, nameof(images));

            var car = new Car(DistrctCode, Brand, Type, Description, images);
            return await _carRepository.InsertAsync(car);
        }
    }
}