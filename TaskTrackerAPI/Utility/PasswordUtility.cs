namespace TaskTrackerAPI.Utility;

public static class PasswordUtility
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("Password cannot be null or empty.");
        }
        else
        {
            var hashpass = BCrypt.Net.BCrypt.HashPassword(password);
            return hashpass;
        }
    }

    public static bool VerifyHashPassword(string password, string passwordHash)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("Password cannot be null or empty.");   
        }
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}