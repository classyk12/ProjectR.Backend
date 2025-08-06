using Newtonsoft.Json;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class SimpleMessageModel : WhatsAppRequestBaseModel
    {
        [JsonProperty("text")]
        public Text? Text;
    }

    public class Text
    {
        [JsonProperty("preview_url")]
        public bool PreviewUrl;

        [JsonProperty("body")]
        public string? Body;
    }
}
