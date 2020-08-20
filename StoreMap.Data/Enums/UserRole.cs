using StoreMap.Data.Attributes;

namespace StoreMap.Data.Enums
{
    public enum UserRole
    {
        [ReferenceCode("User")]
        User,
        [ReferenceCode("Moderator")]
        Moderator,
        [ReferenceCode("Admin")]
        Admin,
        [ReferenceCode(new []{"Admin", "Moderator"})]
        AdminModerator,
        [ReferenceCode(new []{"Admin", "Moderator", "User"})]
        All
    }
}