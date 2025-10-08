using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This is the avaibility for a business for a given period
    /// </summary>
    public class BreakModel : BaseObject
    {
        [Required]
        public Guid BusinessAvailabilitySlotId { get; set; }

        //public BusinessAvailabilitySlot? BusinessAvailabilitySlot { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? StartTime { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? EndTime { get; set; }
    }

    public class AddBreakModel
    {
        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? StartTime { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? EndTime { get; set; }
    }
}

