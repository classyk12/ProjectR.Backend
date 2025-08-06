using Newtonsoft.Json;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class InteractiveMessageModel : WhatsAppRequestBaseModel
    {
        [JsonProperty("interactive")]
        public Interactive? Interactive;
    }
    public class Action
    {
        [JsonProperty("buttons")]
        public List<Button>? Buttons;
    }

    public class Body
    {
        [JsonProperty("text")]
        public string? Text;
    }

    public class Button
    {
        [JsonProperty("type")]
        public string? Type;

        [JsonProperty("reply")]
        public Reply? Reply;
    }

    public class Footer
    {
        [JsonProperty("text")]
        public string? Text;
    }

    public class Header
    {
        [JsonProperty("type")]
        public string? Type;

        [JsonProperty("image")]
        public Image? Image;
    }

    public class Image
    {
        [JsonProperty("id")]
        public string? Id;
    }

    public class Interactive
    {
        [JsonProperty("type")]
        public string? Type;

        [JsonProperty("header")]
        public Header? Header;

        [JsonProperty("body")]
        public Body? Body;

        [JsonProperty("footer")]
        public Footer? Footer;

        [JsonProperty("action")]
        public Action? Action;
    }

    public class Reply
    {
        [JsonProperty("id")]
        public string? Id;

        [JsonProperty("title")]
        public string? Title;
    }
}
