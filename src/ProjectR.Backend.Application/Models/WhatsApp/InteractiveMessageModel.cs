using Newtonsoft.Json;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class InteractiveMessageModel : WhatsAppRequestBaseModel
    {
        [JsonProperty("interactive")]
        public Interactive? Interactive { get; set; }
    }
    public class Action
    {
        [JsonProperty("buttons")]
        public List<Button>? Buttons { get; set; }
    }

    public class Body
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
    }

    public class Button
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("reply")]
        public Reply? Reply { get; set; }
    }

    public class Footer
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
    }

    public class Header
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("image")]
        public Image? Image { get; set; }
    }

    public class Image
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
    }

    public class Interactive
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("header")]
        public Header? Header { get; set; }

        [JsonProperty("body")]
        public Body? Body { get; set; }

        [JsonProperty("footer")]
        public Footer? Footer { get; set; }

        [JsonProperty("action")]
        public Action? Action { get; set; }
    }

    public class Reply
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }
    }
}
