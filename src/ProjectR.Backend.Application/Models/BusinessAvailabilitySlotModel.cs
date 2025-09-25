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

        public BusinessAvailabilityModel? BusinessAvailability { get; set; }

        [Required]
        public TimeOnly? StartTime { get; set; }

        [Required]
        public TimeOnly? EndTime { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        public List<BreakModel>? Breaks { get; set; }
    }

    public class AddBusinessAvailabilitySlotModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeOnly? StartTime { get; set; }

        [Required]
        public TimeOnly? EndTime { get; set; }

        public List<AddBreakModel> Breaks { get; set; } = [];
    }
}

