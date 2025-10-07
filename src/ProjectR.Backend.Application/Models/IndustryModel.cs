using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    public class IndustryModel
    {
            public Guid Id { get; set; }

            public string? Name { get; set; }

            public string? Description { get; set; }
    }

    public class AddIndustryModel
    {

        [Required(ErrorMessage = "Industry Name is Required")]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
