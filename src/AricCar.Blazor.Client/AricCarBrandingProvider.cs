using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using AricCar.Localization;

namespace AricCar.Blazor.Client;

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
