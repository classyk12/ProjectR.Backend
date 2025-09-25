using ProjectR.Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{

    public class BusinessAvailabilityModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public DateOnly? StartDate { get; set; }

        [Required]
        public DateOnly? EndDate { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one slot is required")]
        public ICollection<BusinessAvailabilitySlotModel>? Slots { get; set; }
    }

    /// <summary>
    /// This is a bucket for business availability slots for a given period
    /// </summary>
    public class AddBusinessAvailabilityModel
    {
        [Required]
        public Guid BusinessId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateOnly? StartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateOnly? EndDate { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one slot is required")]
        public List<AddBusinessAvailabilitySlotModel>? Slots { get; set; }

        public List<AddBreakModel>? Breaks { get; set; }
    }

    public class UpdateBusinessAvailabilityModel
    {
        [Required]
        public Guid BusinessId { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one slot is required")]
        public List<AddBusinessAvailabilitySlotModel>? Slots { get; set; }

        public List<AddBreakModel>? Breaks { get; set; }
    }
}

