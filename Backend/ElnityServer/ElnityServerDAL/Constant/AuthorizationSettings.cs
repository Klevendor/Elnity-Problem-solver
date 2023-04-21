namespace ElnityServerDAL.Constant
{
    public static class AuthorizationSettings
    {
        public enum Roles
        {
            Administrator,
            User
        }

        public const string DefaultUsername = "SuperUser";
        public const string DefaultPassword = "password";
        public const string DefaultEmail = "superuser@test.com";
        public const Roles SuperUserRole = Roles.Administrator;
    }
}
