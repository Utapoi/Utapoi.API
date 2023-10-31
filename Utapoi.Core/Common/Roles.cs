namespace Utapoi.Core.Common;

/// <summary>
///     Represents the roles.
/// </summary>
public static class Roles
{
    /// <summary>
    ///     The admin role.
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    ///     The user role.
    /// </summary>
    public const string User = "User";

    /// <summary>
    ///     Concatenates multiple roles into a single string.
    /// </summary>
    public static string Concat(params string[] roles)
    {
        return string.Join(",", roles);
    }
}