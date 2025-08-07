namespace ProjectR.Backend.Application.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// Token Validity in Minutes
        /// </summary>
        public int ValidityInMinutes { get; set; }
    }
}
