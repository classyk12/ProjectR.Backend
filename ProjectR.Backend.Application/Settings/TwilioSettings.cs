namespace ProjectR.Backend.Application.Settings
{
    public class TwilioSettings
    {
        public string AccountSID { get; set; } = string.Empty;
        public string AuthToken { get; set; } = string.Empty;
        /// <summary>
        /// Content SID for OTP messages. This is used to specify the template for OTP messages sent via Twilio.
        /// </summary>
        public string OtpContentSid { get; set; } = string.Empty;
        public bool UseMock { get; set; }
    }
}
