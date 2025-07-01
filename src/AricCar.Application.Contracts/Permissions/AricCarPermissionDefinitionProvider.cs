using AricCar.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AricCar.Permissions;

public class AricCarPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AricCarPermissions.GroupName);

        var fieldKeyValuePairPermission = myGroup.AddPermission(AricCarPermissions.Regions.Default, L("«¯”Úπ‹¿Ì"));
        fieldKeyValuePairPermission.AddChild(AricCarPermissions.Regions.Create, L("Permission:Create"));
        fieldKeyValuePairPermission.AddChild(AricCarPermissions.Regions.Edit, L("Permission:Edit"));
        fieldKeyValuePairPermission.AddChild(AricCarPermissions.Regions.Delete, L("Permission:Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AricCarPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AricCarResource>(name);
    }
}