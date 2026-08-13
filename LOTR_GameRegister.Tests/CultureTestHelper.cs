using System.Globalization;

namespace LOTR_GameRegister.Tests;

/// <summary>
/// Helps tests deal with environments that run in globalization-invariant mode
/// (e.g. CI images without ICU), where named cultures cannot be created.
/// </summary>
internal static class CultureTestHelper
{
    /// <summary>
    /// Returns the requested culture, or <see langword="null"/> when the culture
    /// is unavailable in the current environment (globalization invariant mode).
    /// </summary>
    public static CultureInfo? TryCreate(string name)
    {
        try
        {
            return CultureInfo.GetCultureInfo(name);
        }
        catch (CultureNotFoundException)
        {
            return null;
        }
    }
}
