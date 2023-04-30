namespace ElnityServerDAL.Constant
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double DurationInMinutes { get; set; }

        public int RefreshTokenTTL { get; set; }
    }
}
