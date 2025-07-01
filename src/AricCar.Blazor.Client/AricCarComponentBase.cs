using AricCar.Localization;
using Volo.Abp.AspNetCore.Components;

namespace AricCar.Blazor.Client;

public abstract class AricCarComponentBase : AbpComponentBase
{
    protected AricCarComponentBase()
    {
        LocalizationResource = typeof(AricCarResource);
    }
}
