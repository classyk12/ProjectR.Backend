using Newtonsoft.Json;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class SimpleMessageModel : WhatsAppRequestBaseModel
    {
        [JsonProperty("text")]
        public Text? Text { get; set; }
    }

    public class Text
    {
        [JsonProperty("preview_url")]
        public bool PreviewUrl { get; set; } = false;

        [JsonProperty("body")]
        public string? Body { get; set; }
    }
}
