using AricCar.Localization;
using Volo.Abp.Application.Services;

namespace AricCar;

/* Inherit your application services from this class.
 */
public abstract class AricCarAppService : ApplicationService
{
    protected AricCarAppService()
    {
        LocalizationResource = typeof(AricCarResource);
    }
}
