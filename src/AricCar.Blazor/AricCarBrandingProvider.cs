using Microsoft.Extensions.Localization;
using AricCar.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AricCar.Blazor;

[Dependency(ReplaceServices = true)]
public class AricCarBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AricCarResource> _localizer;

    public AricCarBrandingProvider(IStringLocalizer<AricCarResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
