using ProjectR.Backend.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    public class Appointment : BaseObject
    {
        [Required]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [Required]
        public AppointmentStatus Status { get; set; }
    }
}

