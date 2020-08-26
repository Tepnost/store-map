namespace StoreMap.Data
{
    public static class Constants
    {
        public const string RolesClaim = "https://store-map.com/assignedRoles";
        public const string AdminRole = "admin";
        public const string ModRole = "mod";
        public const string UserRole = "user";
        public const string AdminModRoles = AdminRole + "," + ModRole;
        public const string AllRoles = AdminRole + "," + ModRole + "," + UserRole;

        public const string GenericError = "Oops! Something went wrong...";
    }
}