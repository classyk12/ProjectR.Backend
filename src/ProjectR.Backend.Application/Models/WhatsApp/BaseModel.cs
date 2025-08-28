using Newtonsoft.Json;
using ProjectR.Backend.Shared;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class ResultModel
    {
        [JsonProperty("messaging_product")]
        public string? MessagingProduct { get; set; }

        [JsonProperty("contacts")]
        public List<Contact>? Contacts { get; set; }

        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
    }

    public class Contact
    {
        [JsonProperty("input")]
        public string? Input { get; set; }

        [JsonProperty("wa_id")]
        public string? WaId { get; set; }
    }

    public class Message
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
    }

    public class WhatsAppRequestBaseModel
    {
        [JsonProperty("messaging_product")]
        public string? MessagingProduct { get; set; }

        [JsonProperty("recipient_type")]
        public string? RecipientType { get; set; }

        [JsonProperty("to")]
        public string? To { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}
