using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    public class AppThemeModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class AddAppThemeModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}
