using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Domain.Entities
{
    public class WebhookMessage: BaseObject
    {
        public WebhookSource Source { get; set; }
        public string? Payload { get; set; }
        public bool IsProcessed { get; set; }
        public DateTimeOffset? LastProcessedDate { get; set; }
        public string? LastProcessedResult { get; set; }
    }
}