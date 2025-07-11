using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This is the avaibility for a business for a given period
    /// </summary>
    public class BusinessAvailabilitySlot : BaseObject
    {
        [Required]
        public Guid BusinessAvailabilityId { get; set; }
        public BusinessAvailability? BusinessAvailability { get; set; }
        [Required]
        public TimeOnly? StartTime { get; set; }
        [Required]
        public TimeOnly? EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 

        /// <summary>
        /// keeping the breaks here as a list of time(may be better to extract and compre that way)
        /// This is subject to restructuring if need be in the future
        /// </summary>
        public List<TimeOnly>? Breaks { get; set; }
    }
}

