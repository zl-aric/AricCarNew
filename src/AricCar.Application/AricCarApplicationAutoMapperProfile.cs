using AricCar.Cars;
using AricCar.Regions;
using AutoMapper;

namespace AricCar;

public class AricCarApplicationAutoMapperProfile : Profile
{
    public AricCarApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Region, RegionDto>();
        CreateMap<Car, CarDto>();
    }
}
