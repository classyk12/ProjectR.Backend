using Newtonsoft.Json;
using ProjectR.Backend.Shared;

namespace ProjectR.Backend.Application.Models.WhatsApp
{
    public class Change
    {
        [JsonProperty("value")]
        public Value? Value { get; set; }

        [JsonProperty("field")]
        public string? Field { get; set; }
    }

    public class ContactInfo
    {
        [JsonProperty("profile")]
        public Profile? Profile { get; set; }

        [JsonProperty("wa_id")]
        public string? WaId { get; set; }
    }

    public class Entry
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("changes")]
        public List<Change>? Changes { get; set; }
    }

    public class MessageInfo
    {
        [JsonProperty("from")]
        public string? From { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("text")]
        public Text? Text { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("display_phone_number")]
        public string? DisplayPhoneNumber { get; set; }

        [JsonProperty("phone_number_id")]
        public string? PhoneNumberId { get; set; }
    }

    public class Profile
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class WhatsAppWebhookMessageModel
    {
        [JsonProperty("object")]
        public string? Object { get; set; }

        [JsonProperty("entry")]
        public List<Entry>? Entry { get; set; }
    }

    public class Value
    {
        [JsonProperty("messaging_product")]
        public string? MessagingProduct { get; set; }

        [JsonProperty("metadata")]
        public Metadata? Metadata { get; set; }

        [JsonProperty("contacts")]
        public List<ContactInfo>? Contacts { get; set; }

        [JsonProperty("messages")]
        public List<MessageInfo>? Messages { get; set; }
    }
}
