using ProjectR.Backend.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    public class Industry
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Industry Name is Required")]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}

