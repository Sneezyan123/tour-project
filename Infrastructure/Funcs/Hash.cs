namespace TourProject.Infrastructure.Funcs
{
    public static class Hash
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
        public static bool VerifyPassword(string hashedPassowrd, string password)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(hashedPassowrd, password);
        }

    }
}
