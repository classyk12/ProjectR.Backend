using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This is the avaibility slot for a business for a given period and represents a day in the week
    /// </summary>
    public class BusinessAvailabilitySlot : BaseObject
    {
        [Required]
        public Guid BusinessAvailabilityId { get; set; }
        public BusinessAvailability? BusinessAvailability { get; set; }
        [Required]
        public DateTimeOffset? StartTime { get; set; }
        [Required]
        public DateTimeOffset? EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// The specific date for this slot. Slots are generally recurring weekly but this date represents the specific date for this slot
        /// </summary>
        public DateTimeOffset Date { get; set; }
        public ICollection<Break>? Breaks { get; set; }
    }
}

