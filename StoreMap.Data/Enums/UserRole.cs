using StoreMap.Data.Attributes;

namespace StoreMap.Data.Enums
{
    public enum UserRole
    {
        [ReferenceCode(Constants.UserRole)]
        User,
        [ReferenceCode(Constants.ModRole)]
        Moderator,
        [ReferenceCode(Constants.AdminRole)]
        Admin,
        [ReferenceCode(new []{Constants.AdminRole, Constants.ModRole})]
        AdminMod,
        [ReferenceCode(new []{Constants.AdminRole, Constants.ModRole, Constants.UserRole})]
        All
    }
}