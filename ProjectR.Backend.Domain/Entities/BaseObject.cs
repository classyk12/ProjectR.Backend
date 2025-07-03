namespace ProjectR.Backend.Domain.Entities
{
    public class BaseObject
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public RecordStatus? RecordStatus { get; set; }
    }
}