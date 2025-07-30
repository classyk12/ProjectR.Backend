using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    public class BusinessAvailability : BaseObject
    {
        public Guid BusinessId { get; set; }
        public Business? Business { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        /// <summary>
        /// This will be used in scenerios where we decide to use templating. 
        /// Templating allows a business to reuse a previous availability without necessarily setting it all over again
        /// </summary>
        public DateOnly? ValidFrom { get; set; }
        public DateOnly? ValidTo { get; set; }
        public Collection<BusinessAvailabilitySlot>? Slots { get; set; }
    }
}

