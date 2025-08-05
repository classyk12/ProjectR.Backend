namespace ProjectR.Backend.Application.Settings
{
    public class WhatsappCloudApiSettings
    {
        public string PhoneNumberId { get; set; } = string.Empty;
        public string BusinessAccountId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public bool UseMock { get; set; }
    }
}
