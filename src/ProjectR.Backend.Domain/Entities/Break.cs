using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This is the avaibility for a business for a given period
    /// </summary>
    public class Break : BaseObject
    {
        [Required]
        public Guid BusinessAvailabilitySlotId { get; set; }
        public BusinessAvailabilitySlot? BusinessAvailabilitySlot { get; set; }
        [Required]
        public TimeOnly? StartTime { get; set; }
        [Required]
        public TimeOnly? EndTime { get; set; }
    }
}

