namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This is a bucket for business availability slots for a given period
    /// </summary>
    public class BusinessAvailability : BaseObject
    {
        public Guid BusinessId { get; set; }
        public Business? Business { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<BusinessAvailabilitySlot>? Slots { get; set; }
    }
}

