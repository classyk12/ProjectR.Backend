using Newtonsoft.Json;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class ResultModel
    {
        [JsonProperty("messaging_product")]
        public string? MessagingProduct;

        [JsonProperty("contacts")]
        public List<Contact>? Contacts;

        [JsonProperty("messages")]
        public List<Message>? Messages;
    }

    public class Contact
    {
        [JsonProperty("input")]
        public string? Input;

        [JsonProperty("wa_id")]
        public string? WaId;
    }

    public class Message
    {
        [JsonProperty("id")]
        public string? Id;
    }

    public class WhatsAppRequestBaseModel
    {
        [JsonProperty("messaging_product")]
        public string? MessagingProduct;

        [JsonProperty("recipient_type")]
        public string? RecipientType;

        [JsonProperty("to")]
        public string? To;

        [JsonProperty("type")]
        public string? Type;
    }
}
