using ProjectR.Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    /// <summary>
    /// This is the avaibility slot for a business for a given period and represents a day in the week
    /// </summary>
    public class BusinessAvailabilitySlotModel
    {
        [Required]
        public Guid BusinessAvailabilityId { get; set; }

        //public BusinessAvailabilityModel? BusinessAvailability { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? StartTime { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? EndTime { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTimeOffset Date { get; set; }

        public List<BreakModel>? Breaks { get; set; }
    }

    public class AddBusinessAvailabilitySlotModel
    {
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTimeOffset Date { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? StartTime { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Invalid time format")]
        public DateTimeOffset? EndTime { get; set; }

        // [JsonIgnore]
        // public TimeOnly? StartTimeInternal => TimeOnly.FromDateTime(StartTime!.Value);

        // [JsonIgnore]
        // public TimeOnly? EndTimeInternal => TimeOnly.FromDateTime(EndTime!.Value);

        public List<AddBreakModel> Breaks { get; set; } = [];
    }
}

