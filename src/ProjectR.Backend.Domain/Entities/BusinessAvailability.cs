using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This is a bucket for business availability slots for a given period
    /// </summary>
    public class BusinessAvailability : BaseObject
    {
        public Guid BusinessId { get; set; }
        public Business? Business { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public ICollection<BusinessAvailabilitySlot>? Slots { get; set; }
    }
}

