using AricCar.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AricCar.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AricCarController : AbpControllerBase
{
    protected AricCarController()
    {
        LocalizationResource = typeof(AricCarResource);
    }
}
