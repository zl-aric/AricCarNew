namespace AricCar.Permissions;

public static class AricCarPermissions
{
    public const string GroupName = "AricCar";

    public static class Regions
    {
        public const string Default = GroupName + ".Regions";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}


