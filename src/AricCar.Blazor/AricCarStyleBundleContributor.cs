using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace AricCar.Blazor;

public class AricCarStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add(new BundleFile("main.css", true));
    }
}
