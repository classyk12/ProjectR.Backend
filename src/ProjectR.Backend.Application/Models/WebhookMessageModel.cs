using ProjectR.Backend.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    public class WebhookMessageModel
    {
        public Guid? Id { get; set; }
        public WebhookSource Source { get; set; }
        public string? Payload { get; set; }
        public bool IsProcessed { get; set; }
        public DateTimeOffset? LastProcessedDate { get; set; }
        public string? LastProcessedResult { get; set; }
    }
}
